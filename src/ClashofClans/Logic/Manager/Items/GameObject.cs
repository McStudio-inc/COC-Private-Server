// Location: ClashofClans.Logic.Manager.Items.GameObject.cs
using System.Collections.Generic;
using System.Numerics;
using Newtonsoft.Json.Linq;

namespace ClashofClans.Logic.Manager.Items
{
    public class GameObject
    {
        private const int LayoutCount = 6;

        public List<Component> Components;
        public Home.Home Home;
        public Vector2 Position;
        public int Id;

        // Permanent layout positions (tersimpan ke DB)
        public int[] LayoutX = new int[LayoutCount];
        public int[] LayoutY = new int[LayoutCount];

        // Edit mode layout positions (sementara saat drag, belum di-save)
        public int[] EditLayoutX = new int[LayoutCount];
        public int[] EditLayoutY = new int[LayoutCount];

        public GameObject(Home.Home home)
        {
            Home = home;
            Position = Vector2.Zero;
            Components = new List<Component>();

            for (int i = 0; i < LayoutCount; i++)
            {
                LayoutX[i] = -1;
                LayoutY[i] = -1;
                EditLayoutX[i] = -1;
                EditLayoutY[i] = -1;
            }
        }

        /// <summary>
        ///     Set posisi layout. editMode=true = edit mode (sementara), false = permanent.
        /// </summary>
        public void SetPositionLayoutXY(int x, int y, int layoutId, bool editMode = false)
        {
            if (layoutId < 0 || layoutId >= LayoutCount) return;

            if (editMode)
            {
                EditLayoutX[layoutId] = x;
                EditLayoutY[layoutId] = y;
            }
            else
            {
                LayoutX[layoutId] = x;
                LayoutY[layoutId] = y;
            }
        }

        /// <summary>
        ///     Get posisi layout. editMode=true = edit mode, false = permanent.
        /// </summary>
        public (int x, int y) GetPositionLayout(int layoutId, bool editMode = false)
        {
            if (layoutId < 0 || layoutId >= LayoutCount) return (-1, -1);

            return editMode
                ? (EditLayoutX[layoutId], EditLayoutY[layoutId])
                : (LayoutX[layoutId], LayoutY[layoutId]);
        }

        /// <summary>
        ///     Width dalam tiles — override di subclass jika perlu
        /// </summary>
        public virtual int GetWidthInTiles() => 1;

        /// <summary>
        ///     Height dalam tiles — override di subclass jika perlu
        /// </summary>
        public virtual int GetHeightInTiles() => 1;

        protected void SaveLayoutPositions(JObject jObject)
        {
            var lx = new JArray();
            var ly = new JArray();
            var elx = new JArray();
            var ely = new JArray();

            bool hasLayout = false;
            bool hasEditLayout = false;

            for (int i = 0; i < LayoutCount; i++)
            {
                lx.Add(LayoutX[i]);
                ly.Add(LayoutY[i]);
                elx.Add(EditLayoutX[i]);
                ely.Add(EditLayoutY[i]);

                if (LayoutX[i] != -1 || LayoutY[i] != -1) hasLayout = true;
                if (EditLayoutX[i] != -1 || EditLayoutY[i] != -1) hasEditLayout = true;
            }

            if (hasLayout)
            {
                jObject.Add("lx", lx);
                jObject.Add("ly", ly);
            }

            if (hasEditLayout)
            {
                jObject.Add("elx", elx);
                jObject.Add("ely", ely);
            }
        }

        protected void LoadLayoutPositions(JObject jObject)
        {
            if (jObject.ContainsKey("lx") && jObject.ContainsKey("ly"))
            {
                var lx = jObject["lx"] as JArray;
                var ly = jObject["ly"] as JArray;

                if (lx != null && ly != null)
                    for (int i = 0; i < LayoutCount && i < lx.Count; i++)
                    {
                        LayoutX[i] = lx[i].ToObject<int>();
                        LayoutY[i] = ly[i].ToObject<int>();
                    }
            }

            if (jObject.ContainsKey("elx") && jObject.ContainsKey("ely"))
            {
                var elx = jObject["elx"] as JArray;
                var ely = jObject["ely"] as JArray;

                if (elx != null && ely != null)
                    for (int i = 0; i < LayoutCount && i < elx.Count; i++)
                    {
                        EditLayoutX[i] = elx[i].ToObject<int>();
                        EditLayoutY[i] = ely[i].ToObject<int>();
                    }
            }
        }

        public virtual JObject Save()
        {
            var jObject = new JObject
            {
                {"x", (int) Position.X},
                {"y", (int) Position.Y}
            };

            SaveLayoutPositions(jObject);

            foreach (var component in Components)
                component.Save(jObject);

            return jObject;
        }

        public void AddComponent(Component component)
        {
            if (!Components.Contains(component))
                Components.Add(component);

            Home.ComponentManager.AddComponent(component);
        }

        public bool TryGetComponent(int type, out Component component)
        {
            component = Components.Find(t => t.Type == type);
            return component != null;
        }

        public virtual void Load(JObject jObject)
        {
            var x = jObject["x"].ToObject<int>();
            var y = jObject["y"].ToObject<int>();
            Position = new Vector2(x, y);

            LoadLayoutPositions(jObject);

            foreach (var component in Components)
                component.Load(jObject);
        }

        public virtual void FastForward(int seconds)
        {
            foreach (var component in Components) component.FastForward(seconds);
        }

        public virtual void Tick()
        {
            foreach (var component in Components) component.Tick();
        }
    }
}
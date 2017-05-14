using System.Collections.Generic;

namespace HospitalCeo.UI
{
    public static class UIManager
    {
        private static Dictionary<string, Menu> openInterfaces;

        private static void Initialise()
        {
            openInterfaces = new Dictionary<string, Menu>();
        }

        public static void Create(string menuName, Menu menu)
        {
            if (openInterfaces == null)
                Initialise();

            openInterfaces.Add(menuName, menu);
            menu.Show();
        }

        public static bool Destory(string menuName)
        {
            Menu menu;
            openInterfaces.TryGetValue(menuName, out menu);
            if (menu != null)
            {
                menu.Destory();
                menu = null;
                openInterfaces.Remove(menuName);
                return true;
            }

            return false;
        }

        public static bool Show(string menuName)
        {
            Menu menu;
            openInterfaces.TryGetValue(menuName, out menu);

            if (menu != null)
            {
                menu.Show();
                return true;
            }

            return false;
        }

        public static bool Hide(string menuName)
        {
            Menu menu;
            openInterfaces.TryGetValue(menuName, out menu);

            if (menu != null)
            {
                menu.Hide();
                return true;
            }

            return false;
        }

        public static bool Toggle(string menuName)
        {
            Menu menu;
            openInterfaces.TryGetValue(menuName, out menu);

            if (menu != null)
            {
                menu.Toggle();
                return true;
            }

            return false;
        }

        public static void Update()
        {
            if (openInterfaces == null)
                return;

            foreach (Menu m in openInterfaces.Values)
                m.Update();
        }
    }
}

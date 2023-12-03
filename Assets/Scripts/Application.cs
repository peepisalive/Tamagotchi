namespace Tamagotchi
{
    public static class Application
    {
        public static Model Model { get; private set; }

        static Application()
        {
            Model = new Model();
        }
    }
}
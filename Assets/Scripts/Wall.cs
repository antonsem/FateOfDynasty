namespace GGJ21
{
    public class Wall : Item
    {
        private void OnEnable()
        {
            Events.Instance.bookPressed += Use;
        }

        private void OnDisable()
        {
            if(GameState.IsQuitting) return;
            Events.Instance.bookPressed -= Use;
        }

        public override void Clicked()
        {
        }
    }
}
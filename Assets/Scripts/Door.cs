namespace GGJ21
{
    public class Door : Entity
    {
        public override void Clicked()
        {
            gameObject.SetActive(false);
        }
    }
}
namespace UI
{
    public class SLTab : ADollTalkWindowTab
    {
        public override void OpenTab()
        {
            gameObject.SetActive(true);
        }

        public override void CloseTab()
        {
            gameObject.SetActive(false);
        }

        public override void GetInput(InputType input)
        {
            throw new System.NotImplementedException();
        }
    }
}
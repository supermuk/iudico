namespace IUDICO.DataModel.Controllers
{
    public abstract class PageControllerBase
    {
        public virtual void LoadState(object state)
        {
        }

        public virtual object SaveState()
        {
            return null;
        }
    }
}

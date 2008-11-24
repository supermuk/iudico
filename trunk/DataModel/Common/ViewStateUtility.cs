namespace IUDICO.DataModel.Common
{
    public interface IViewStateSerializable
    {
        object SaveViewStateData();
        void LoadViewStateData(object data);
    }
}

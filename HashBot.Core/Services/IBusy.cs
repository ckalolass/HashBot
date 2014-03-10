namespace HashBot.Core.Services
{
    public interface IBusy
    {
        bool IsBusy { get; }

        void SetBusy(bool value);
    }
}

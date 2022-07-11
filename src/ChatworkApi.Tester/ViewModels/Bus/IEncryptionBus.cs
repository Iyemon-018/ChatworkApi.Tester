namespace ChatworkApi.Tester.ViewModels.Bus
{
    using Domain;

    public interface IEncryptionBus
    {
        IEncryption Encryption { get; }

        IDecryption Decryption { get; }
    }
}
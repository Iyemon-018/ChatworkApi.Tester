namespace ChatworkApi.Tester.ViewModels.Bus
{
    using Domain;

    public sealed class EncryptionBus : IEncryptionBus
    {
        public EncryptionBus(IEncryption encryption
                           , IDecryption decryption)
        {
            Encryption = encryption;
            Decryption = decryption;
        }

        public IEncryption Encryption { get; }

        public IDecryption Decryption { get; }
    }
}
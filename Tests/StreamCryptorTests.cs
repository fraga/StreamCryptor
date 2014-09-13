﻿using NUnit.Framework;
using Sodium;
using System.IO;

namespace Tests
{
    [TestFixture]
    public class StreamCryptorTests
    {
        /// <summary>
        /// EncryptFileWithStream and DecryptFileWithStream
        ///</summary>
        [Test]
        public void StreamCryptorUsualTest()
        {
            const string RAW_FILE = "Testfiles\\MyAwesomeChipmunkKiller.jpg";
            const string ENCRYPTED_FILE = "Testfiles\\MyAwesomeChipmunkKiller.jpg.encrypted";
            const string DECRYPTED_FILE = "Testfiles\\decrypted\\MyAwesomeChipmunkKiller.jpg";
            const string DECRYPTED_OUTPUT_DIRECTORY = "Testfiles\\decrypted";
            string PRIVATE_KEY = "1158b1ea7d45919968b87dab6cab27eff5871304ea9856588e9ec02a6d93c42e";
            string PUBLIC_KEY = "1158b1ea7d45919968b87dab6cab27eff5871304ea9856588e9ec02a6d93c42e";
            KeyPair keyPair = new KeyPair(Utilities.HexToBinary(PUBLIC_KEY), Utilities.HexToBinary(PRIVATE_KEY));

            StreamCryptor.StreamCryptor.EncryptFileWithStream(keyPair, RAW_FILE, false);
            StreamCryptor.StreamCryptor.DecryptFileWithStream(keyPair, ENCRYPTED_FILE, DECRYPTED_OUTPUT_DIRECTORY);

            //get a SHA256 checksum to validate our work, this takes us currently ~100ms.
            Assert.AreEqual(StreamCryptor.Helper.Utils.GetChecksum(RAW_FILE), StreamCryptor.Helper.Utils.GetChecksum(DECRYPTED_FILE));
 
            //clear garbage 
            File.Delete(ENCRYPTED_FILE);
            File.Delete(DECRYPTED_FILE);
        }
    }
}
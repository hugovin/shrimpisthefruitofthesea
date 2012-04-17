using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

using System.IO;
using System.Text;

/// Pulled from http://blogs.microsoft.co.il/blogs/kim/archive/2009/01/23/pgp-zip-encrypted-files-with-c.aspx
/// Modified to fit ER on June 5, 2009
namespace ER.Common.Encryption
{
    public class PgpEncryptionKeys
    {
        private PgpPublicKey _PublicKey;
        private PgpPrivateKey _PrivateKey;
        private PgpSecretKey _SecretKey;

        public PgpPublicKey PublicKey
        {
            get { return this._PublicKey; }
            private set { this._PublicKey = value; }
        }
        public PgpPrivateKey PrivateKey
        {
            get { return this._PrivateKey; }
            private set { this._PrivateKey = value; }
        }
        public PgpSecretKey SecretKey
        {
            get { return this._SecretKey; }
            private set { this._SecretKey = value; }
        }

        /// <summary>
        /// Initializes a new instance of the EncryptionKeys class.
        /// The data is encrypted with the recipients public key.
        /// </summary>
        /// <param name="publicKeyPath">The key used to encrypt the data</param>
        /// <exception cref="ArgumentException">Public key not found.</exception>
        public PgpEncryptionKeys(string publicKeyPath)
        {
            if (!File.Exists(publicKeyPath))
                throw new ArgumentException("Public key file not found", "publicKeyPath");
            PublicKey = ReadPublicKey(publicKeyPath);
        }

        /// <summary>
        /// Initializes a new instance of the EncryptionKeys class.
        /// Two keys are required to encrypt and sign data. Your private key and the recipients public key.
        /// The data is encrypted with the recipients public key and signed with your private key.
        /// </summary>
        /// <param name="publicKeyPath">The key used to encrypt the data</param>
        /// <param name="privateKeyPath">The key used to sign the data.</param>
        /// <param name="passPhrase">The (your) password required to access the private key</param>
        /// <exception cref="ArgumentException">Public key not found. Private key not found. Missing password</exception>
        public PgpEncryptionKeys(string publicKeyPath, string privateKeyPath, string passPhrase)
        {
            if (!File.Exists(publicKeyPath))
                throw new ArgumentException("Public key file not found", "publicKeyPath");
            if (!File.Exists(privateKeyPath))
                throw new ArgumentException("Private key file not found", "privateKeyPath");
            if (String.IsNullOrEmpty(passPhrase))
                throw new ArgumentException("passPhrase is null or empty.", "passPhrase");
            PublicKey = ReadPublicKey(publicKeyPath);
            SecretKey = ReadSecretKey(privateKeyPath);
            PrivateKey = ReadPrivateKey(passPhrase);
        }

        #region Secret Key
        private PgpSecretKey ReadSecretKey(string privateKeyPath)
        {
            using (Stream keyIn = File.OpenRead(privateKeyPath))
            using (Stream inputStream = PgpUtilities.GetDecoderStream(keyIn))
            {
                PgpSecretKeyRingBundle secretKeyRingBundle = new PgpSecretKeyRingBundle(inputStream);
                PgpSecretKey foundKey = GetFirstSecretKey(secretKeyRingBundle);
                if (foundKey != null)
                    return foundKey;
            }
            throw new ArgumentException("Can't find signing key in key ring.");
        }

        /// <summary>
        /// Return the first key we can use to encrypt.
        /// Note: A file can contain multiple keys (stored in "key rings")
        /// </summary>
        private PgpSecretKey GetFirstSecretKey(PgpSecretKeyRingBundle secretKeyRingBundle)
        {
            foreach (PgpSecretKeyRing kRing in secretKeyRingBundle.GetKeyRings())
            {
                PgpSecretKey key = null;
                foreach(PgpSecretKey k in kRing.GetSecretKeys())
                {
                    /*.Cast<PgpSecretKey>()
                    .Where(k >= k.IsSigningKey)
                    .FirstOrDefault();*/
                    if (k.IsSigningKey)
                    {
                        key = k;
                        break;
                    }
                }
                if (key != null)
                    return key;
            }
            return null;
        }
        #endregion

        #region Public Key
        private PgpPublicKey ReadPublicKey(string publicKeyPath)
        {
            using (Stream keyIn = File.OpenRead(publicKeyPath))
            using (Stream inputStream = PgpUtilities.GetDecoderStream(keyIn))
            {
                PgpPublicKeyRingBundle publicKeyRingBundle = new PgpPublicKeyRingBundle(inputStream);
                PgpPublicKey foundKey = GetFirstPublicKey(publicKeyRingBundle);
                if (foundKey != null)
                    return foundKey;
            }
            throw new ArgumentException("No encryption key found in public key ring.");
        }

        private PgpPublicKey GetFirstPublicKey(PgpPublicKeyRingBundle publicKeyRingBundle)
        {
            foreach (PgpPublicKeyRing kRing in publicKeyRingBundle.GetKeyRings())
            {
                PgpPublicKey key = null;
                foreach (PgpPublicKey k in kRing.GetPublicKeys())
                {
                    /*= kRing.GetPublicKeys()
                   .Cast<PgpPublicKey>()
                   .Where(k => k.IsEncryptionKey)
                   .FirstOrDefault();*/
                    if (k.IsEncryptionKey)
                    {
                        key = k;
                        break;
                    }
                }

                if (key != null)
                    return key;
            }
            return null;
        }
        #endregion

        #region Private Key
        private PgpPrivateKey ReadPrivateKey(string passPhrase)
        {
            PgpPrivateKey privateKey = SecretKey.ExtractPrivateKey(passPhrase.ToCharArray());
            if (privateKey != null)
                return privateKey;
            throw new ArgumentException("No private key found in secret key.");
        }
        #endregion

        public static void GenerateKeys(string username, string password, string keyStoreUrl)
        {
            IAsymmetricCipherKeyPairGenerator kpg = new RsaKeyPairGenerator();
            kpg.Init(new RsaKeyGenerationParameters(BigInteger.ValueOf(0x13), new SecureRandom(), 1024, 8));
            AsymmetricCipherKeyPair kp = kpg.GenerateKeyPair();

            FileStream out1 = new FileInfo(string.Format("{0}secret.asc", keyStoreUrl)).OpenWrite();
            FileStream out2 = new FileInfo(string.Format("{0}pub.asc", keyStoreUrl)).OpenWrite();

            ExportKeyPair(out1, out2, kp.Public, kp.Private, username, password.ToCharArray(), true);
        }

        private static void ExportKeyPair(
            Stream secretOut,
            Stream publicOut,
            AsymmetricKeyParameter publicKey,
            AsymmetricKeyParameter privateKey,
            string identity,
            char[] passPhrase,
            bool armor)
        {
            if (armor)
            {
                secretOut = new ArmoredOutputStream(secretOut);
            }

            PgpSecretKey secretKey = new PgpSecretKey(
                PgpSignature.DefaultCertification,
                PublicKeyAlgorithmTag.RsaGeneral,
                publicKey,
                privateKey,
                DateTime.Now,
                identity,
                SymmetricKeyAlgorithmTag.Cast5,
                passPhrase,
                null,
                null,
                new SecureRandom()
                //                ,"BC"
                );

            secretKey.Encode(secretOut);
            secretOut.Close();

            if (armor)
            {
                publicOut = new ArmoredOutputStream(publicOut);
            }

            PgpPublicKey key = secretKey.PublicKey;
            key.Encode(publicOut);
            publicOut.Close();
        }

    }

    #region PGPEncrypt
    /// <summary>
    /// Wrapper around Bouncy Castle OpenPGP library.
    /// Bouncy documentation can be found here: http://www.bouncycastle.org/docs/pgdocs1.6/index.html
    /// </summary>
    public class PgpEncrypt
    {
        private PgpEncryptionKeys m_encryptionKeys;
        private const int BufferSize = 0x10000; // should always be power of 2 

        /// <summary>
        /// Instantiate a new PgpEncrypt class with initialized PgpEncryptionKeys.
        /// </summary>
        /// <param name="encryptionKeys"></param>
        /// <exception cref="ArgumentNullException">encryptionKeys is null</exception>
        public PgpEncrypt(PgpEncryptionKeys encryptionKeys)
        {
            if (encryptionKeys == null)
                throw new ArgumentNullException("encryptionKeys", "encryptionKeys is null.");
            m_encryptionKeys = encryptionKeys;
        }

        /// <summary>
        /// Encrypt and sign the file pointed to by unencryptedFileInfo and
        /// write the encrypted content to outputStream.
        /// </summary>
        /// <param name="outputStream">The stream that will contain the
        /// encrypted data when this method returns.</param>
        /// <param name="fileName">FileInfo of the file to encrypt</param>
        public void EncryptAndSignFile(Stream outputStream, FileInfo unencryptedFileInfo)
        {
            if (outputStream == null)
                throw new ArgumentNullException("outputStream", "outputStream is null.");
            if (unencryptedFileInfo == null)
                throw new ArgumentNullException("unencryptedFileInfo", "unencryptedFileInfo is null.");
            if (!File.Exists(unencryptedFileInfo.FullName))
                throw new ArgumentException("File to encrypt not found.");
            using (Stream encryptedOut = ChainEncryptedOut(outputStream))
            using (Stream compressedOut = ChainCompressedOut(encryptedOut))
            {
                PgpSignatureGenerator signatureGenerator = InitSignatureGenerator(compressedOut);
                using (Stream literalOut = ChainLiteralOut(compressedOut, unencryptedFileInfo))
                using (FileStream inputFile = unencryptedFileInfo.OpenRead())
                {
                    WriteOutputAndSign(compressedOut, literalOut, inputFile, signatureGenerator);
                }
            }
        }

        private static void WriteOutputAndSign(Stream compressedOut,
            Stream literalOut,
            FileStream inputFile,
            PgpSignatureGenerator signatureGenerator)
        {
            int length = 0;
            byte[] buf = new byte[BufferSize];
            while ((length = inputFile.Read(buf, 0, buf.Length)) > 0)
            {
                literalOut.Write(buf, 0, length);
                signatureGenerator.Update(buf, 0, length);
            }
            signatureGenerator.Generate().Encode(compressedOut);
        }

        private Stream ChainEncryptedOut(Stream outputStream)
        {
            PgpEncryptedDataGenerator encryptedDataGenerator;
            encryptedDataGenerator =
                new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.TripleDes,
                                              new SecureRandom());
            encryptedDataGenerator.AddMethod(m_encryptionKeys.PublicKey);
            return encryptedDataGenerator.Open(outputStream, new byte[BufferSize]);
        }

        private static Stream ChainCompressedOut(Stream encryptedOut)
        {
            PgpCompressedDataGenerator compressedDataGenerator =
                new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip);
            return compressedDataGenerator.Open(encryptedOut);
        }

        private static Stream ChainLiteralOut(Stream compressedOut, FileInfo file)
        {
            PgpLiteralDataGenerator pgpLiteralDataGenerator = new PgpLiteralDataGenerator();
            return pgpLiteralDataGenerator.Open(compressedOut, PgpLiteralData.Binary, file);
        }

        private PgpSignatureGenerator InitSignatureGenerator(Stream compressedOut)
        {
            const bool IsCritical = false;
            const bool IsNested = false;
            PublicKeyAlgorithmTag tag = m_encryptionKeys.SecretKey.PublicKey.Algorithm;
            PgpSignatureGenerator pgpSignatureGenerator =
                new PgpSignatureGenerator(tag, HashAlgorithmTag.Sha1);
            pgpSignatureGenerator.InitSign(PgpSignature.BinaryDocument, m_encryptionKeys.PrivateKey);
            foreach (string userId in m_encryptionKeys.SecretKey.PublicKey.GetUserIds())
            {
                PgpSignatureSubpacketGenerator subPacketGenerator =
                   new PgpSignatureSubpacketGenerator();
                subPacketGenerator.SetSignerUserId(IsCritical, userId);
                pgpSignatureGenerator.SetHashedSubpackets(subPacketGenerator.Generate());
                // Just the first one!
                break;
            }
            pgpSignatureGenerator.GenerateOnePassVersion(IsNested).Encode(compressedOut);
            return pgpSignatureGenerator;
        }
    }
    #endregion

    #region PGPWrapper
    public class PGPWrapper
    {
        #region PGP Encryption Algorithms
        private static PgpPublicKey ReadPublicKey(Stream inputStream)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);
            PgpPublicKeyRingBundle pgpPub = new PgpPublicKeyRingBundle(inputStream);

            foreach (PgpPublicKeyRing kRing in pgpPub.GetKeyRings())
            {
                foreach (PgpPublicKey k in kRing.GetPublicKeys())
                {
                    if (k.IsEncryptionKey)
                    {
                        return k;
                    }
                }
            }

            throw new ArgumentException("Can't find encryption key in key ring.");
        }

        private static PgpPrivateKey FindSecretKey(
            Stream keyIn,
            long keyId,
            char[] pass)
        {
            PgpSecretKeyRingBundle pgpSec = new PgpSecretKeyRingBundle(
                PgpUtilities.GetDecoderStream(keyIn));

            PgpSecretKey pgpSecKey = pgpSec.GetSecretKey(keyId);

            if (pgpSecKey == null)
            {
                return null;
            }

            return pgpSecKey.ExtractPrivateKey(pass);
        }

        private static void EncryptFile(
            Stream outputStream,
            string fileName,
            PgpPublicKey encKey,
            bool armor,
            bool withIntegrityCheck)
        {
            if (armor)
            {
                outputStream = new ArmoredOutputStream(outputStream);
            }

            try
            {
                MemoryStream bOut = new MemoryStream();

                PgpCompressedDataGenerator comData = new PgpCompressedDataGenerator(
                    CompressionAlgorithmTag.Zip);

                PgpUtilities.WriteFileToLiteralData(
                    comData.Open(bOut),
                    PgpLiteralData.Binary,
                    new FileInfo(fileName));

                comData.Close();

                PgpEncryptedDataGenerator cPk = new PgpEncryptedDataGenerator(
                    SymmetricKeyAlgorithmTag.Cast5, withIntegrityCheck, new SecureRandom());

                cPk.AddMethod(encKey);

                byte[] bytes = bOut.ToArray();

                Stream cOut = cPk.Open(outputStream, bytes.Length);

                cOut.Write(bytes, 0, bytes.Length);

                cOut.Close();

                if (armor)
                {
                    outputStream.Close();
                }
            }
            catch (PgpException e)
            {
                Console.Error.WriteLine(e);

                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {
                    Console.Error.WriteLine(underlyingException.Message);
                    Console.Error.WriteLine(underlyingException.StackTrace);
                }
            }
        }

        private static Stream DecryptFileAsStream(
            Stream inputStream,
            Stream keyIn,
            char[] passwd)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);
            MemoryStream outputStream = new MemoryStream();

            try
            {
                PgpObjectFactory pgpF = new PgpObjectFactory(inputStream);
                PgpEncryptedDataList enc;

                PgpObject o = pgpF.NextPgpObject();
                //
                // the first object might be a PGP marker packet.
                //
                if (o is PgpEncryptedDataList)
                {
                    enc = (PgpEncryptedDataList)o;
                }
                else
                {
                    enc = (PgpEncryptedDataList)pgpF.NextPgpObject();
                }

                //
                // find the secret key
                //
                PgpPrivateKey sKey = null;
                PgpPublicKeyEncryptedData pbe = null;

                foreach (PgpPublicKeyEncryptedData pked in enc.GetEncryptedDataObjects())
                {
                    sKey = FindSecretKey(keyIn, pked.KeyId, passwd);

                    if (sKey != null)
                    {
                        pbe = pked;
                        break;
                    }
                }

                //                Iterator                    it = enc.GetEncryptedDataObjects();
                //
                //                while (sKey == null && it.hasNext())
                //                {
                //                    pbe = (PgpPublicKeyEncryptedData)it.next();
                //
                //                    sKey = FindSecretKey(keyIn, pbe.KeyID, passwd);
                //                }

                if (sKey == null)
                {
                    throw new ArgumentException("secret key for message not found.");
                }

                Stream clear = pbe.GetDataStream(sKey);

                PgpObjectFactory plainFact = new PgpObjectFactory(clear);

                PgpObject message = plainFact.NextPgpObject();

                if (message is PgpCompressedData)
                {
                    PgpCompressedData cData = (PgpCompressedData)message;
                    PgpObjectFactory pgpFact = new PgpObjectFactory(cData.GetDataStream());

                    message = pgpFact.NextPgpObject();

                    if (message is PgpOnePassSignatureList)
                    {
                        //throw new PgpException("encrypted message contains a signed message - not literal data.");
                        //
                        // file is signed!
                        //

                        // verify signature here if you want.
                        //
                        PgpOnePassSignatureList p1 = (PgpOnePassSignatureList)message;
                        PgpOnePassSignature ops = p1[0];
                        // etc…

                        message = pgpFact.NextPgpObject();
                    }
                }
                if (message is PgpLiteralData)
                {
                    PgpLiteralData ld = (PgpLiteralData)message;

                    Stream unc = ld.GetInputStream();

                    int ch;
                    while ((ch = unc.ReadByte()) >= 0)
                    {
                        outputStream.WriteByte((byte)ch);
                    }

                    outputStream.Seek(0, SeekOrigin.Begin);
                }
                else
                {
                    throw new PgpException("message is not a simple encrypted file - type unknown.");
                }

                if (pbe.IsIntegrityProtected())
                {
                    if (!pbe.Verify())
                    {
                        Console.Error.WriteLine("message failed integrity check");
                    }
                    else
                    {
                        Console.Error.WriteLine("message integrity check passed");
                    }
                }
                else
                {
                    Console.Error.WriteLine("no message integrity check");
                }

                return outputStream;
            }
            catch (PgpException e)
            {
                Console.Error.WriteLine(e);

                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {
                    Console.Error.WriteLine(underlyingException.Message);
                    Console.Error.WriteLine(underlyingException.StackTrace);
                }

                throw e;
            }
        }

        private static void DecryptFile(
            Stream inputStream,
            Stream keyIn,
            char[] passwd,
            string tempDir)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);

            try
            {
                PgpObjectFactory pgpF = new PgpObjectFactory(inputStream);
                PgpEncryptedDataList enc;

                PgpObject o = pgpF.NextPgpObject();
                //
                // the first object might be a PGP marker packet.
                //
                if (o is PgpEncryptedDataList)
                {
                    enc = (PgpEncryptedDataList)o;
                }
                else
                {
                    enc = (PgpEncryptedDataList)pgpF.NextPgpObject();
                }

                //
                // find the secret key
                //
                PgpPrivateKey sKey = null;
                PgpPublicKeyEncryptedData pbe = null;

                foreach (PgpPublicKeyEncryptedData pked in enc.GetEncryptedDataObjects())
                {
                    sKey = FindSecretKey(keyIn, pked.KeyId, passwd);

                    if (sKey != null)
                    {
                        pbe = pked;
                        break;
                    }
                }

                //                Iterator                    it = enc.GetEncryptedDataObjects();
                //
                //                while (sKey == null && it.hasNext())
                //                {
                //                    pbe = (PgpPublicKeyEncryptedData)it.next();
                //
                //                    sKey = FindSecretKey(keyIn, pbe.KeyID, passwd);
                //                }

                if (sKey == null)
                {
                    throw new ArgumentException("secret key for message not found.");
                }

                Stream clear = pbe.GetDataStream(sKey);

                PgpObjectFactory plainFact = new PgpObjectFactory(clear);

                PgpObject message = plainFact.NextPgpObject();

                if (message is PgpCompressedData)
                {
                    PgpCompressedData cData = (PgpCompressedData)message;
                    PgpObjectFactory pgpFact = new PgpObjectFactory(cData.GetDataStream());

                    message = pgpFact.NextPgpObject();
                }

                if (message is PgpOnePassSignatureList)
                {
                    //throw new PgpException("encrypted message contains a signed message - not literal data.");
                    //
                    // file is signed!
                    //

                    // verify signature here if you want.
                    //
                    // PGPOnePassSignatureList p1 = (PGPOnePassSignatureList) message;
                    // PGPOnePassSignature ops = p1.get(0);
                    // etc…

                    message = plainFact.NextPgpObject();
                }

                if (message is PgpLiteralData)
                {
                    PgpLiteralData ld = (PgpLiteralData)message;

                    //System.Diagnostics.EventLog.WriteEntry("PGPWrapper[Decrypt] DEBUG", "Decrypting to: " + ld.FileName);
                    using (FileStream fOut = File.Create(Path.Combine(tempDir, ld.FileName)))
                    {
                        //System.Diagnostics.EventLog.WriteEntry("PGPWrapper[Decrypt] DEBUG", "Decrypted to: " + fOut.Name);

                        Stream unc = ld.GetInputStream();

                        int ch;
                        while ((ch = unc.ReadByte()) >= 0)
                        {
                            fOut.WriteByte((byte)ch);
                        }

                        fOut.Close();
                    }
                }
                else
                {
                    throw new PgpException("message is not a simple encrypted file - type unknown.");
                }

                if (pbe.IsIntegrityProtected())
                {
                    if (!pbe.Verify())
                    {
                        Console.Error.WriteLine("message failed integrity check");
                    }
                    else
                    {
                        Console.Error.WriteLine("message integrity check passed");
                    }
                }
                else
                {
                    Console.Error.WriteLine("no message integrity check");
                }
            }
            catch (PgpException e)
            {
                Console.Error.WriteLine(e);

                Exception underlyingException = e.InnerException;
                if (underlyingException != null)
                {
                    Console.Error.WriteLine(underlyingException.Message);
                    Console.Error.WriteLine(underlyingException.StackTrace);
                }
            }
        }
        #endregion PGP Encryption Algorithms

        /**
        * verify the passed in file as being correctly signed.
        */
        public static void VerifyFile(
            Stream inputStream,
            Stream keyIn)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);

            PgpObjectFactory pgpFact = new PgpObjectFactory(inputStream);
            PgpCompressedData c1 = (PgpCompressedData)pgpFact.NextPgpObject();
            pgpFact = new PgpObjectFactory(c1.GetDataStream());

            PgpOnePassSignatureList p1 = (PgpOnePassSignatureList)pgpFact.NextPgpObject();

            PgpOnePassSignature ops = p1[0];

            PgpLiteralData p2 = (PgpLiteralData)pgpFact.NextPgpObject();
            Stream dIn = p2.GetInputStream();
            PgpPublicKeyRingBundle pgpRing = new PgpPublicKeyRingBundle(PgpUtilities.GetDecoderStream(keyIn));
            PgpPublicKey key = pgpRing.GetPublicKey(ops.KeyId);
            FileStream fos = File.Create(p2.FileName);

            ops.InitVerify(key);

            int ch;
            while ((ch = dIn.ReadByte()) >= 0)
            {
                ops.Update((byte)ch);
                fos.WriteByte((byte)ch);
            }
            fos.Close();

            PgpSignatureList p3 = (PgpSignatureList)pgpFact.NextPgpObject();
            PgpSignature firstSig = p3[0];
            if (ops.Verify(firstSig))
            {
                Console.Out.WriteLine("signature verified.");
            }
            else
            {
                Console.Out.WriteLine("signature verification failed.");
            }
        }

        public static Stream EncryptStream(Stream inStream, string pubKeyFile, string sourceFilename, string extension, bool armorFlag, bool integrityCheckFlag)
        {
            string tmpEncryptedFile = sourceFilename + "." + extension;

            try
            {
                using (StreamWriter sourceStream = new StreamWriter(sourceFilename))
                {
                    using (StreamReader sr = new StreamReader(inStream))
                    {
                        sourceStream.Write(sr.ReadToEnd());
                    }
                }

                using (FileStream encryptedStream = File.Create(tmpEncryptedFile))
                {
                    PgpPublicKey publicKey = ReadPublicKey(File.OpenRead(pubKeyFile));
                    EncryptFile(encryptedStream, sourceFilename, publicKey, armorFlag, integrityCheckFlag);

                    encryptedStream.Seek(0, SeekOrigin.Begin);
                    int myCapacity = Int32.Parse(encryptedStream.Length.ToString());
                    Byte[] byteArray = new Byte[myCapacity];

                    MemoryStream memStream = new MemoryStream();

                    encryptedStream.Read(byteArray, 0, myCapacity);
                    memStream.Write(byteArray, 0, myCapacity);

                    encryptedStream.Close();

                    memStream.Seek(0, SeekOrigin.Begin);
                    return memStream;
                }
            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("PGPWrapper[Encrypt] Error", ex.ToString());
                throw ex;
            }
            finally
            {
                // Clean-up temp files
                if (File.Exists(sourceFilename)) { File.Delete(sourceFilename); }
                if (File.Exists(tmpEncryptedFile)) { File.Delete(tmpEncryptedFile); }

                //FileInfo fileInfo = new FileInfo(sourceFilename);
                //System.Diagnostics.EventLog.WriteEntry("PGP_Debug", fileInfo.FullName + " was deleted");

                //fileInfo = new FileInfo(tmpEncryptedFile);
                //System.Diagnostics.EventLog.WriteEntry("PGP_Debug", fileInfo.FullName + " was deleted");
            }
        }

        public static Stream DecryptStream(Stream inStream, string privKeyFile, string passphrase, string encryptedFilename, string tempDir)
        {
            // Remove last extension from encrypted filename.  Could be PGP, ASC, etc
            string tmpDecryptedFile = encryptedFilename.Substring(0, encryptedFilename.LastIndexOf("."));

            try
            {
                //System.Diagnostics.EventLog.WriteEntry("PGPWrapper[Decrypt] DEBUG", "tmpDecryptedFile = " + tmpDecryptedFile);
                using (FileStream keyIn = File.OpenRead(privKeyFile))
                {
                    Stream decryptedStream = DecryptFileAsStream(inStream, keyIn, passphrase.ToCharArray());
                    decryptedStream.Seek(0, SeekOrigin.Begin);
                    return decryptedStream;
                }
            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("PGPWrapper[Decrypt] Error", ex.ToString());
                throw ex;
            }
            finally
            {
                // Clean-up temp files
            }
        }
    }
    #endregion

    public class PGPMessage
    {
        public static string EncryptMessage(string message, string pub_key_path, bool armor, bool integrityCheck)
        {
            MemoryStream input_stream = new MemoryStream(UnicodeEncoding.Default.GetBytes(message));
	    Stream output_stream = PGPWrapper.EncryptStream(input_stream, pub_key_path, "c:\\Windows\\temp\\message.txt", "txt", armor, integrityCheck);

            StreamReader reader = new StreamReader(output_stream);
            return reader.ReadToEnd();
        }

        public static string DecryptMessage(string message, string priv_key_path, string passphrase)
        {
            MemoryStream input_stream = new MemoryStream(UnicodeEncoding.Default.GetBytes(message));
            Stream output_stream = PGPWrapper.DecryptStream(input_stream, priv_key_path, passphrase, "message.enc.txt", "");
            StreamReader reader = new StreamReader(output_stream);
            return reader.ReadToEnd();
        }
    }

}
package ca.csf.mobile1.tp2.modele;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

/**
 * Created by Devilclown1607 on 2017-04-03.
 */
public class EncryptionCypherTest {
    char[] inputCharacters = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
            'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ', '.'};

    char[] outputCharacters = new char[] {'.', ' ', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P',
            'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'z', 'y', 'x', 'w', 'v', 'u', 't', 's', 'r', 'q', 'p',
            'o', 'n', 'm', 'l', 'k', 'j', 'i', 'h', 'g', 'f', 'e', 'd', 'c', 'b', 'a'};
    @Test
    public void getInputMessage() throws Exception {

        EncryptionCypher encryptionCypher = new EncryptionCypher();
        String cryptedMessage = encryptionCypher.encryptMessage("abcde",inputCharacters,outputCharacters);

        assertEquals(". ZYX", cryptedMessage);
    }

    @Test
    public void getValidInputCharacters() throws Exception {
        EncryptionCypher encryptionCypher = new EncryptionCypher();
        String cryptedMessage = encryptionCypher.encryptMessage("abcde",inputCharacters,outputCharacters);

        assertEquals(encryptionCypher.getValidInputCharacters(), inputCharacters);
    }

    @Test
    public void getValidOutputCharacters() throws Exception {
        EncryptionCypher encryptionCypher = new EncryptionCypher();
        String cryptedMessage = encryptionCypher.encryptMessage("abcde",inputCharacters,outputCharacters);

        assertEquals(encryptionCypher.getValidOutputCharacters(), outputCharacters);
    }

    @Test
    public void skipUnknownCharactersWhileCrypting() throws Exception {
        EncryptionCypher encryptionCypher = new EncryptionCypher();
        String cryptedMessage = encryptionCypher.encryptMessage("?$!",inputCharacters,outputCharacters);

        assertEquals("", cryptedMessage);
    }

    @Test
    public void cryptBlankMessage() throws Exception {
        EncryptionCypher encryptionCypher = new EncryptionCypher();
        String cryptedMessage = encryptionCypher.encryptMessage("",inputCharacters,outputCharacters);

        assertEquals("", cryptedMessage);
    }

    @Test
    public void cryptNullMessage() throws Exception {
        EncryptionCypher encryptionCypher = new EncryptionCypher();
        String cryptedMessage = encryptionCypher.encryptMessage(null,inputCharacters,outputCharacters);

        assertEquals("", cryptedMessage);
    }





}
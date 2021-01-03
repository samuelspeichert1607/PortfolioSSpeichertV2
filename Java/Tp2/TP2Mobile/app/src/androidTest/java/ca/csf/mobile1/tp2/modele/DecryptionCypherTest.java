package ca.csf.mobile1.tp2.modele;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

/**
 * Created by Devilclown1607 on 2017-04-06.
 */
public class DecryptionCypherTest {
    char[] inputCharacters = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
            'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ', '.'};

    char[] outputCharacters = new char[] {'.', ' ', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P',
            'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'z', 'y', 'x', 'w', 'v', 'u', 't', 's', 'r', 'q', 'p',
            'o', 'n', 'm', 'l', 'k', 'j', 'i', 'h', 'g', 'f', 'e', 'd', 'c', 'b', 'a'};
    @Test
    public void getInputMessage() throws Exception {

        DecryptionCypher decryptionCypher = new DecryptionCypher();
        String cryptedMessage = decryptionCypher.decryptMessage(". ZYX",inputCharacters,outputCharacters);
        assertEquals("abcde", cryptedMessage);
    }

    @Test
    public void getValidInputCharacters() throws Exception {
        DecryptionCypher decryptionCypher = new DecryptionCypher();
        String cryptedMessage = decryptionCypher.decryptMessage(". ZYX",inputCharacters,outputCharacters);
        assertEquals(decryptionCypher.getValidInputCharacters(), inputCharacters);
    }

    @Test
    public void getValidOutputCharacters() throws Exception {
        DecryptionCypher decryptionCypher = new DecryptionCypher();
        String cryptedMessage = decryptionCypher.decryptMessage(". ZYX",inputCharacters,outputCharacters);
        assertEquals(decryptionCypher.getValidOutputCharacters(), outputCharacters);
    }

    @Test
    public void skipUnknownCharactersWhileDecrypting() throws Exception {
        DecryptionCypher decryptionCypher = new DecryptionCypher();
        String cryptedMessage = decryptionCypher.decryptMessage("?$!",inputCharacters,outputCharacters);
        assertEquals("", cryptedMessage);
    }

    @Test
    public void decryptBlankMessage() throws Exception {
        DecryptionCypher decryptionCypher = new DecryptionCypher();
        String cryptedMessage = decryptionCypher.decryptMessage("",inputCharacters,outputCharacters);
        assertEquals("", cryptedMessage);
    }

    @Test
    public void decryptNullMessage() throws Exception {
        DecryptionCypher decryptionCypher = new DecryptionCypher();
        String cryptedMessage = decryptionCypher.decryptMessage(null,inputCharacters,outputCharacters);
        assertEquals("", cryptedMessage);
    }
}
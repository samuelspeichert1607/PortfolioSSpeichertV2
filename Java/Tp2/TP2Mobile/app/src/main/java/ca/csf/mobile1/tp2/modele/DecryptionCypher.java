package ca.csf.mobile1.tp2.modele;

/**
 * Created by Samuel Speichert and Alexandre Lachance on 2017-04-03.
 */

/**
 * Cette classe sert, en tant que modèle, à décrypter un message.
 */
public class DecryptionCypher {
    private String inputMessage;

    private char[] validInputCharacters;
    private char[] validOutputCharacters;

    /** Cette fonction servira à décrypter le message recu en paramètre.
     * @param inputMessage Le message que l'on désire décrypter
     * @param validInputCharacters Les caractères originaux de l'alphabet.
     * @param validOutputCharacters Les caractères de cryptage.
     * @return Le message décrypté.
     */
    public String decryptMessage(String inputMessage, char[] validInputCharacters, char[] validOutputCharacters){
        String outputMessage="";
        this.inputMessage = inputMessage;
        this.validInputCharacters=validInputCharacters;
        this.validOutputCharacters=validOutputCharacters;

        if(inputMessage != null) {
            for (int i = 0; i < inputMessage.length(); i++) {
                for (int j = 0; j < validInputCharacters.length - 1; j++) {
                    if (inputMessage.charAt(i) == validOutputCharacters[j]) {
                        outputMessage += validInputCharacters[j];
                    }
                }
            }
        }
        else {
            inputMessage = "";
        }
        return outputMessage;
    }

    /**
     * Cet accesseur sert à obtenir le message à crypter.
     * Est utilisé dans les tests unitaires.
     * @return Ceci va retourner le message que l'on tente de crypter.
     */
    public String getInputMessage() {
        return inputMessage;
    }

    /**
     * Cet accesseur sert à obtenir le tableau des caractères de base du message
     * à crypter.
     * Est utilisé dans les tests unitaires.
     * @return Ceci va retourner le message que l'on tente de crypter.
     */
    public char[] getValidInputCharacters() {
        return validInputCharacters;
    }

    /**
     * Cet accesseur sert à obtenir le tableau des caractères du language
     * de cryptage.
     * Est utilisé dans les tests unitaires.
     * @return Ceci va retourner le message que l'on tente de crypter.
     */
    public char[] getValidOutputCharacters() {
        return validOutputCharacters;
    }
}

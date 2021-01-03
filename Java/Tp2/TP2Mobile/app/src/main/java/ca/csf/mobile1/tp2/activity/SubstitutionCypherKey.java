package ca.csf.mobile1.tp2.activity;

/**
 * Created by Samuel Speichert and Alexandre Lachance on 2017-03-27.
 */

    public class SubstitutionCypherKey
    {
        private int key = 0;
        private char[] inputCharacters; //Les charactères valides que l'utilisateur pourra entré (recu par le networkTask)
        private char[] outputCharacters; //Les charactères de décryption correspondant à la clé choisi (recu par le networkTask)

        public SubstitutionCypherKey(int key, char[] inputCharacters, char[] outputCharacters) {
            this.key = key;
            this.inputCharacters = inputCharacters;
            this.outputCharacters = outputCharacters;
        }

        /**
         * Accesseur pour obtenir la clé de chiffrement.
         * @return Clé de chiffrement.
         */
        public int getKey() {
            return key;
        }

        /**
         * Accesseur pour obtenir le tableau des caractères de l'alphabet original.
         * @return Tableau des caractères de l'alphabet original.
         */
        public char[] getInputCharacters() {
            return inputCharacters;
        }

        /**
         * Accesseur pour obtenir le tableau des caractères de l'alphabet de cryptage.
         * @return Tableau des caractères de l'alphabet de cryptage.
         */
        public char[] getOutputCharacters() {
            return outputCharacters;
        }
    }


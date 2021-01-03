package ca.csf.mobile1.tp1.controller;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.mockito.ArgumentCaptor;

import java.io.InputStream;
import java.io.PipedInputStream;
import java.io.PipedOutputStream;
import java.io.PrintStream;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

//import ca.csf.mobile1.tp1.chemical.Main;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.fail;
import static org.mockito.Matchers.anyInt;
import static org.mockito.Mockito.*;

public class ApplicationTest {

    //<editor-fold desc="Unit Tests Preparation">

    private InputStream systemInBackup;
    private PipedOutputStream systemIn;

    private PrintStream systemOutBackup;
    private PrintStream systemOut;

    private Thread applicationThread;

    @Before
    public void before() throws Exception {
        systemInBackup = System.in;
        systemIn = new PipedOutputStream();

        systemOutBackup = System.out;
        systemOut = spy(System.out);

        System.setIn(new PipedInputStream(systemIn));
        System.setOut(systemOut);

        applicationThread = new ApplicationThread();
        applicationThread.start();
    }

    @After
    public void after() throws Exception {
        //For testing purposes, deprecation is ignored
        //noinspection deprecation
        applicationThread.stop();

        System.setIn(systemInBackup);
        System.setOut(systemOutBackup);
    }

    //</editor-fold>

    @Test
    public void askForChemicalElementAtStart() throws Exception {
        assertMessage("Entrez une formule chimique :");
    }

    @Test
    public void canComputeChemicalCompoundWeight1() throws Exception {
        writeTextToConsole("H2O");

        assertOutput("H20", 18.01528D);
    }

    @Test
    public void canComputeChemicalCompoundWeight2() throws Exception {
        writeTextToConsole("NaCl");

        assertOutput("NaCl", 58.4428D);
    }

    @Test
    public void canComputeChemicalCompoundWeight3() throws Exception {
        writeTextToConsole("(NaCl)4");

        assertOutput("(NaCl)4", 233.7711D);
    }

    @Test
    public void canComputeChemicalCompoundWeight4() throws Exception {
        writeTextToConsole("(H2SO4(Be)3(H2O))2");

        assertOutput("(H2SO4(Be)3(H2O))2", 286.2606D);
    }

    @Test
    public void cannotComputeEmptyChemicalCompoundFormula() throws Exception {
        writeTextToConsole("");

        assertError("La formule saisie est vide.");
    }

    @Test
    public void cannotComputeChemicalCompoundFormulaWithOnlyWhitespaces() throws Exception {
        writeTextToConsole("      ");

        assertError("La formule saisie est vide.");
    }

    @Test
    public void cannotComputeChemicalCompoundFormulaWithOnlyTabs() throws Exception {
        writeTextToConsole("\t\t");

        assertError("La formule saisie est vide.");
    }

    @Test
    public void cannotComputeChemicalCompoundFormulaWithOnlyCarriageRetunrs() throws Exception {
        writeTextToConsole("\n\n");

        assertError("La formule saisie est vide.");
    }

    @Test
    public void cannotComputeChemicalCompoundWithIllegalCharacter() throws Exception {
        writeTextToConsole("H2$");

        assertError("Le caractère \"$\" est illégal dans une formule chimique.");
    }

    @Test
    public void cannotComputeChemicalCompoundWithMisplacedExponent() throws Exception {
        writeTextToConsole("2H2O");

        assertError("Un exposant invalide est placé avant même un élément chimique ou une parenthèse.");
    }

    @Test
    public void cannotComputeChemicalCompoundWithEmptyParenthesis() throws Exception {
        writeTextToConsole("H2()O");

        assertError("Il y a une parenthèse vide invalide.");
    }

    @Test
    public void cannotComputeChemicalCompoundWithUnknownChemicalElement() throws Exception {
        writeTextToConsole("Hu2");

        assertError("L'élément chimique \"Hu\" est inconnu.");
    }

    @Test
    public void cannotComputeChemicalCompoundWithIllegalClosingParenthesis() throws Exception {
        writeTextToConsole("H2O)");

        assertError("Il y a une parenthèse fermante sans sa parenthèse ouvrante.");
    }

    @Test
    public void cannotComputeChemicalCompoundWithMissingClosingParenthesis() throws Exception {
        writeTextToConsole("(H2O");

        assertError("Il y a une parenthèse ouvrante sans sa parenthèse fermante.");
    }

    //<editor-fold desc="Unit Tests Tools">

    private void writeTextToConsole(String text) throws Exception {
        systemIn.write((text + "\n").getBytes());
        systemIn.flush();
    }

    private void assertMessage(String expected) throws Exception {
        waitForConsoleOutput();

        ArgumentCaptor<byte[]> captor = ArgumentCaptor.forClass(byte[].class);
        verify(systemOut, times(1)).write(captor.capture(), anyInt(), anyInt());

        assertEquals(expected, new String(captor.getValue()).trim());
    }

    private void assertError(String expected) throws Exception {
        waitForConsoleOutput();

        ArgumentCaptor<byte[]> captor = ArgumentCaptor.forClass(byte[].class);
        verify(systemOut, times(2)).write(captor.capture(), anyInt(), anyInt());

        assertEquals(expected, new String(captor.getValue()).trim());
    }

    private void assertOutput(String chemicalFormula, double expected) throws Exception {
        waitForConsoleOutput();

        ArgumentCaptor<byte[]> captor = ArgumentCaptor.forClass(byte[].class);
        verify(systemOut, times(2)).write(captor.capture(), anyInt(), anyInt());

        Pattern pattern = Pattern.compile("Le poids de .+? est (\\d+[\\.,]?\\d*) g\\/mol.");
        String consoleOutput = new String(captor.getValue()).trim();
        Matcher matcher = pattern.matcher(consoleOutput);
        if (matcher.matches()) {
            double actual = Double.valueOf(matcher.group(1).replace(",", "."));
            assertEquals(expected, actual, 0.01D);

        } else {
            fail(String.format("Expected output wasn't printed to console.\n\tExpected : \"Le poids de %s est %f g/mol.\"\n\tActual : \"%s\"",
                               chemicalFormula, expected, consoleOutput));
        }
    }

    private void waitForConsoleOutput() throws Exception {
        systemOut.flush();
        Thread.sleep(1000); //Enough for testing purposes
    }

    private static class ApplicationThread extends Thread {
        @Override
        public void run() {
            try {
                Main.main(null);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    //</editor-fold>

}
package ca.csf.mobile1.tp1.activity;

import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import java.io.InputStream;
import java.util.ArrayList;

import ca.csf.mobile1.tp1.R;
import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompound;
import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompoundFactory;
import ca.csf.mobile1.tp1.chemical.compound.EmptyFormulaException;
import ca.csf.mobile1.tp1.chemical.compound.EmptyParenthesisException;
import ca.csf.mobile1.tp1.chemical.compound.IllegalClosingParenthesisException;
import ca.csf.mobile1.tp1.chemical.compound.MisplacedExponentException;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElementRepository;
import ca.csf.mobile1.tp1.controler.ChemicalControler;
import ca.csf.mobile1.tp1.controler.Listener;
import ca.csf.mobile1.tp1.view.ChemicalCompoundCalculatorConsoleView;

public class MainActivity extends AppCompatActivity {

    private View rootView;
    private EditText inputEditText;
    private TextView outputTextView;
    private final ArrayList<Listener> listeners = new ArrayList<Listener>();
    ChemicalElementRepository chemicalElementRepository;
    ChemicalCompoundFactory chemicalCompoundFactory = new ChemicalCompoundFactory(chemicalElementRepository);
    private String input = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        rootView = findViewById(R.id.rootView);
        inputEditText = (EditText) findViewById(R.id.inputEditText);
        outputTextView = (TextView) findViewById(R.id.outputTextView);

        try
        {
            InputStream inputStream = getResources().openRawResource(R. raw. chemical_elements) ;
            chemicalElementRepository = new ChemicalElementRepository(inputStream);

        }
        catch(Exception e)
        {
            super.onDestroy();
        }

    }

    public String getInput() {
        return input;
    }

    public void onComputeButtonClicked(View view) throws Exception
    {
		//TODO : Pour l'instant, n'affiche qu'un message d'erreur
        try
        {
            ChemicalCompound chemicalCompound = chemicalCompoundFactory.createFromString(inputEditText.getText().toString());
            input = inputEditText.getText().toString();


            if(input == "" || input == " ")
            {
                throw new EmptyFormulaException();
            }
            if(input.contains("()"))
            {

                throw new EmptyParenthesisException();
            }
            if ((input.contains(")") && !input.contains("(")))
            {

                throw new MisplacedExponentException();
            }
            if((input.contains("(") && !input.contains(")")))
            {

                throw new IllegalClosingParenthesisException();
            }

            outputTextView.setText(String.format(getResources().getString(R.string.text_output),chemicalCompound.getWeight()));


        }
        catch (EmptyFormulaException e)
        {
            showError(getResources().getString(R.string.text_empty_formula));
        }
        catch (EmptyParenthesisException e)
        {
            showError(getResources().getString(R.string.text_empty_parenthesis));
        }
        catch (MisplacedExponentException e)
        {
            showError(getResources().getString(R.string.text_misplaced_exponent));
        }
        catch (IllegalClosingParenthesisException e)
        {
            showError(getResources().getString(R.string.text_missing_closing_parenthesis));
        }



    }

    private void showError( String message) {
        Snackbar.make(rootView,message,Snackbar.LENGTH_LONG).show();
    }

}

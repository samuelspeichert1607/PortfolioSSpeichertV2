package ca.csf.mobile1.tikiappclient;

import android.content.Context;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.support.test.InstrumentationRegistry;
import android.support.test.filters.LargeTest;
import android.support.test.rule.ActivityTestRule;
import android.support.test.runner.AndroidJUnit4;

import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import ca.csf.mobile1.tikiappclient.model.Cocktail;
import ca.csf.mobile1.tikiappclient.model.Ingredient;
import ca.csf.mobile1.tikiappclient.model.IngredientCocktail;
import ca.csf.mobile1.tikiappclient.service.ApplicationDatabaseHelper;
import ca.csf.mobile1.tikiappclient.service.SQLiteCocktailRepository;
import ca.csf.mobile1.tikiappclient.service.SQLiteIngredientsCocktailRepository;

import static android.support.test.espresso.Espresso.onView;
import static android.support.test.espresso.action.ViewActions.click;
import static android.support.test.espresso.assertion.ViewAssertions.matches;
import static android.support.test.espresso.matcher.ViewMatchers.withId;
import static android.support.test.espresso.matcher.ViewMatchers.withText;
import static org.junit.Assert.assertEquals;

/**
 * Instrumentation test, which will execute on an Android device.
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
@RunWith(AndroidJUnit4.class)
@LargeTest
public class CocktailActivityTest {
    @Rule
    public ActivityTestRule<CocktailActivity> activityRule = new ActivityTestRule<>(CocktailActivity.class, false, false);

    private void show() {
        activityRule.launchActivity(null);
    }

    private void show(int idCocktail) {
        Intent intent = new Intent();
        CocktailActivity.configureIntent(intent, new Cocktail(idCocktail,"","","",""));
        activityRule.launchActivity(intent);
    }

    @Test
    public void checkIfReturnButtonWorks() throws Exception{
        show(1);
        onView(withId(R.id.return_button)).perform(click());
    }

    @Test
    public void checkThatTheRightNameIsDisplayedLongIsland() throws Exception{
        show(1);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Long Island Iced Tea")));
    }

    @Test
    public void checkThatTheRightNameIsDisplayedCowgirl() throws Exception{
        show(2);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Cowgirl")));
    }

    @Test
    public void checkThatTheRightNameIsDisplayedBeach() throws Exception{
        show(3);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Sex on The Beach")));
    }

    @Test
    public void checkThatTheRightNameIsDisplayedMargarita() throws Exception{
        show(4);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Margarita")));
    }

    @Test
    public void checkThatTheRightNameIsDisplayedAmaretto() throws Exception{
        show(5);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Amaretto Sour")));
    }

    @Test
    public void checkThatTheRightNameIsDisplayedSparrow() throws Exception{
        show(6);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Jack Sparrow")));
    }

    @Test
    public void checkThatTheRightNameIsDisplayedMojito() throws Exception{
        show(7);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Mojito")));
    }

    @Test
    public void checkThatTheRightNameIsDisplayedFiring() throws Exception{
        show(8);
        onView(withId(R.id.cocktail_name_textView)).check(matches(withText("Mexican Firing Squad")));
    }
}

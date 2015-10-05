/*
James Felts
AllTests file run this and all tests will run
 */
package fraction;

import org.junit.runner.RunWith;
import org.junit.runners.Suite;
import org.junit.runners.Suite.SuiteClasses;

@RunWith(Suite.class)
@SuiteClasses({ FractionTest.class, IllegalArgument.class })
public class AllTests {

}

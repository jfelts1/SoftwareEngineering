package fraction;

import java.math.BigInteger;

/**
 * Created by james on 10/2/15.
 */
public class Fraction implements Comparable<Fraction>
{
    public Fraction(long num, long den)
    {
        if(den == 0)
        {
            throw new IllegalArgumentException("The denominator with the value of 0 is not permitted");
        }
        if(num < 0)
        {
            m_sign = !m_sign;
        }
        if(den < 0)
        {
            m_sign = !m_sign;
        }
        if(m_sign)
        {
            m_num = Math.abs(num);
            m_den = Math.abs(den);
        }
        else
        {
            m_num = num;
            m_den = den;
        }
        reduceFraction();
    }

    public long getDen()
    {
        if(m_sign)
        {
            return m_den;
        }
        return getSignAsInt()*m_den;
    }
    public long getNum()
    {
        if(m_sign)
        {
            return m_num;
        }
        return getSignAsInt()*m_num;
    }
    public boolean getSign() {return m_sign;}

    public Fraction add(Fraction fraction)
    {
        if(fraction == null)
        {
            throw new NullPointerException("Cannot perform math operations on a null fraction object!");
        }
        long num,den;
        int sign1 = getSignAsInt();
        int sign2 = fraction.getSignAsInt();
        num = sign1*m_num*fraction.m_den+sign2*m_den*fraction.m_num;
        den = m_den*fraction.m_num;
        return new Fraction(num,den);
    }

    public Fraction multiply(Fraction fraction)
    {
        if(fraction == null)
        {
            throw new NullPointerException("Cannot perform math operations on a null fraction object!");
        }
        long num,den;
        int sign1 = getSignAsInt();
        int sign2 = fraction.getSignAsInt();
        num = sign1*sign2*m_num*fraction.m_num;
        den = m_den*fraction.m_den;
        return new Fraction(num,den);
    }

   public double realValue()
    {
        return (double)m_num/(double)m_den;
    }

    @Override
    public int compareTo(Fraction fraction)
    {
        int ret;
        if(m_num==fraction.m_num && m_den == fraction.m_den)
        {
            ret = 0;
        }
        else if(m_num*fraction.m_den < m_den*fraction.m_num)
        {
            ret = -1;
        }
        //can only be equal less than or greater than
        else
        {
            ret = 1;
        }

        return ret;
    }

    @Override
    public String toString()
    {
        StringBuilder ret = new StringBuilder();
        if(!m_sign)
        {
            ret.append('-');
        }
        ret.append(m_num);
        ret.append('/');
        ret.append(m_den);
        return ret.toString();
    }

    @Override
    public boolean equals(Object o)
    {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        Fraction fraction = (Fraction) o;

        if (m_den != fraction.m_den) return false;
        if (m_num != fraction.m_num) return false;
        return m_sign == fraction.m_sign;
    }

    @Override
    public int hashCode() {
        int result = (int) (m_den ^ (m_den >>> 32));
        result = 31 * result + (int) (m_num ^ (m_num >>> 32));
        result = 31 * result + (m_sign ? 1 : 0);
        return result;
    }

    /////privates start here////
    private long m_den;
    private long m_num;
    //true positive false negative
    private boolean m_sign = true;

    private void reduceFraction()
    {
        long gcd = gcd(m_num,m_den);
        m_num  = m_num / gcd;
        m_den = m_den /gcd;
    }

    //http://stackoverflow.com/a/4009230 for how to find the GCD of two numbers in java
    private static long gcd(long val1, long val2)
    {
        BigInteger b1 = BigInteger.valueOf(val1);
        BigInteger b2 = BigInteger.valueOf(val2);
        return b1.gcd(b2).longValue();
    }

    private int getSignAsInt()
    {
        return m_sign?1:-1;
    }
}

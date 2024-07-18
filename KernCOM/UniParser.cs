using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace KernDriver
{
    public class UniParser
    {
        private static char[] ch_num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static String[] str_units={"g", "kg"};
        private static CultureInfo ci = BuildCultureInfo();

        private static CultureInfo BuildCultureInfo()
        {
            CultureInfo ci = new CultureInfo("en-US");
            ci.NumberFormat.NumberDecimalSeparator = ".";
            ci.NumberFormat.NumberGroupSeparator = "";
            return ci;
        }

        private static Boolean IsSupportedUnit(String units)
        {
            if (units == null)
                return false;
            return str_units.Contains(units.ToLower());
        }

        private static double WeightToGramm(double weight, String unit)
        {
            switch (unit)
            {
                case "g": return weight;
                case "kg": return weight * 1000;
                case "lb": return weight * 453.592;
                case "oz": return weight * 28.35;
                default: return weight;
            }
        }
    
        public static Response Parse(String data, String units_default)
        {
            /* Check for empty data string */
            if (data.Length < 1)
                throw new StreamDecoderException("Empty data.");

            /* Trim data string */
            data = data.Trim();

            /* Locate dot */
            int pos_dot=data.IndexOf('.');
            if (pos_dot < 0)
                return Response.Error;

            /* Locate left bound */
            int lb = pos_dot, lbi = lb;
            while ((lb > 0) && (ch_num.Contains(data[lb - 1])))
                lb--;

            /* Locate right bound */
            int rb = pos_dot, rbi=rb;
            while ((rb < data.Length - 1) && (ch_num.Contains(data[rb + 1])))
                    rb++;

            /* Check bounds haven't changed */
            if ((lb == lbi) || (rb == rbi))
                return Response.Error;

            /* Extract weight and units */
            string str_w = data.Substring(lb, rb - lb + 1),
                    str_u = data.Substring(rb + 1).Trim().ToLower();

            /* Prepare response */
            Response resp = new Response();

            /* Parse number */
            try
            {
                resp.Weight=double.Parse(str_w, ci);
            }
            catch (Exception)
            {
                return Response.Error;
            }

            /* Check for unit has been parsed */
            resp.Type = (IsSupportedUnit(str_u)) ? ResponseType.Stable : ResponseType.Unstable;

            /* Convert and return weight if stable */
            if (resp.Type == ResponseType.Stable) {
                resp.Weight = WeightToGramm(resp.Weight, str_u);
                return resp;
            }

            /* Return error if unstable and no default unit specified */
            if ((resp.Type == ResponseType.Unstable) &&
                (units_default == null))
                return Response.Error;

            /* Convert and return weight if unstable and default units specified and supported */
            if ((resp.Type == ResponseType.Unstable) &&
                (IsSupportedUnit(units_default)))
            {
                resp.Weight = WeightToGramm(resp.Weight, units_default);
                return resp;
            }

            /* Return error */
            return Response.Error;
        }

        public static Response Parse(String data)
        {
            return Parse(data, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace createSQLLIB.generate
{
    public enum SimulateTypeEum
    {
        noSimulate,
        FixedValue,
        RangeValue,
        IncrementValue,
        WordsValues
    }

    public abstract class SimulateType
    {
        public abstract string[] Data { get; }
        public abstract int DataLength { get; }

        public abstract SimulateTypeEum Type { get; }
    }

    public class NoSimulate : SimulateType
    {
        public override int DataLength
        {
            get
            {
                return 0;
            }
        }

        public override string[] Data
        {
            get
            {
                return [];
            }
        }

        public override SimulateTypeEum Type
        {
            get
            {
                return SimulateTypeEum.noSimulate;
            }
        }
    }

    public class FixedValue : SimulateType
    {
        private readonly string _value;

        public FixedValue(string fixedValue)
        {
            _value = fixedValue;
        }

        public override int DataLength
        {
            get
            {
                return 1;
            }
        }

        public override string[] Data
        {
            get
            {
                return [_value];
            }
        }

        public override SimulateTypeEum Type
        {   get
            {
                return SimulateTypeEum.FixedValue;
            }
        }
    }

    public class RangeValue : SimulateType
    {
        private readonly string _min;
        private readonly string _max;

        public RangeValue(int min, int max)
        {
            _max = max.ToString();
            _min = min.ToString();
        }

        public override int DataLength
        {
            get
            {
                return 2;
            }
        }

        public override string[] Data
        {
            get
            {
                return [_min, _max];
            }
        }

        public override SimulateTypeEum Type
        {
            get
            {
                return SimulateTypeEum.RangeValue;
            }
        }
    }

    public class IncrementValue : SimulateType
    {
        private readonly string _start;
        private readonly string _step;

        public IncrementValue(int start, int step)
        {
            _start = start.ToString();
            _step = step.ToString();
        }

        public override int DataLength
        {
            get
            {
                return 3;
            }
        }

        public override string[] Data
        {
            get
            {
                return [_start, _step];
            }
        }

        public override SimulateTypeEum Type
        {
            get
            {
                return SimulateTypeEum.FixedValue;
            }
        }
    }

    public class WordsValues : SimulateType
    {
        private readonly string _wordsName;

        public WordsValues(string wordsName)
        {
            _wordsName = wordsName;
        }

        public override int DataLength
        {
            get
            {
                return 1;
            }
        }

        public override string[] Data
        {
            get
            {
                return [_wordsName];
            }
        }

        public override SimulateTypeEum Type
        {
            get
            {
                return SimulateTypeEum.FixedValue;
            }
        }
    }
}

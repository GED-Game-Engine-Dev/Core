using GED.Core.SanityCheck;

namespace GED.Core
{
    /// <summary>
    /// is a list with converter and restricted access to real data. <br/>
    /// has a responsibility to manage the memory it's holding. <br/><br/>
    /// 
    /// 
    /// 
    /// Seemingly for users, this class would take the data as <see cref="I"/>
    /// and give the output value as <see cref="O"/>
    /// while not revealing the existance of <see cref="S"/>.<br/><br/>
    /// 
    /// The main idea of this is as following: <br/>
    /// - Take control of only data would be stored. <br/>
    /// - Hide the data stored. <br/><br/>
    /// 
    /// </summary>
    /// <typeparam name="I">
    /// Constructing value to make <see cref="S"/>.
    /// <see cref="ItoS"/>
    /// </typeparam>
    /// <typeparam name="S">
    /// The type that will be actually stored. <br/>
    /// Host class <see cref="Mgr(I, S, O)"/> will take its memory responsibility. <br/><br/>
    /// 
    /// The values will be used to make an output value as <see cref="O"/>
    /// </typeparam>
    /// <typeparam name="O">
    /// How a data would be actually shown for those who use this.
    /// <see cref="StoO"/>
    /// </typeparam>
    public abstract class Mgr<I, S, O>
    {

        #region Member Fields

        /// <summary>
        /// Here, actual data would be stored so make sure
        /// the garbage collector would not pass them to end.
        /// </summary>
        internal List<S> list;

        #endregion

        #region Properties

        /// <summary>
        /// Represents the length of <see cref="list"/>.
        /// </summary>
        public int Length { get => list.Count; }

        #endregion

        #region Constructors

        public Mgr()
        {
            list = new();
        }

        #endregion

        #region Abstract Functions

        /// <summary>
        /// Use <see cref="I"/> to construct whole new type of <see cref="S"/>.
        /// </summary>
        /// <param name="_in">
        /// Constructing value.
        /// Note that 
        /// </param>
        /// <param name="_el">
        /// Buffer for the output.
        /// </param>
        /// <returns>
        /// <seealso cref="States"/> <br/>
        /// Will contain the flag on failing situation.
        /// </returns>
        abstract protected int ItoS(in I _in, out S? _el);

        /// <summary>
        /// Use <see cref="S"/> to construct whole new type of <see cref="O"/>
        /// </summary>
        /// <param name="_el">
        /// Constructing value.
        /// </param>
        /// <param name="_out">
        /// Buffer for the output.
        /// </param>
        /// <returns>
        /// <seealso cref="States"/> <br/>
        /// Will contain the flag on failing situation.
        /// </returns>
        abstract protected int StoO(in S _el, out O? _out);

        #endregion

        #region Public Functions

        /// <param name="idx">
        /// Index where the construction would occur. <br/><br/>
        /// 
        /// Passing the value greater than <see cref="Length"/> 
        /// would cause <see cref="States.WRONG_OPERATION"/>
        /// </param>
        /// 
        /// <returns>
        /// <see cref="States"/> <br/><br/>
        /// 
        /// Possibly contains the result on conversion. <br/>
        /// See <see cref="ItoS"/>.
        /// </returns>
        /// <exception cref="States.PTR_IS_NULL"/>
        /// <exception cref="States.WRONG_OPERATION"/>
        public int Emplace(int idx, in I raw)
        {
            S? s;
            int r = ItoS(in raw, out s);

            if(r != States.OK && (r & States.DONE_HOWEV) == 0)
            {
                return r;
            }

            if(s == null)
            {
                return States.PTR_IS_NULL;
            }

            if(idx >= list.Count)
            {
                return States.WRONG_OPERATION | r & ~States.DONE_HOWEV;
            }
            
            list[idx] = s;
            return r;
        }

        /// <summary>
        /// <see cref="Emplace"/> <br/><br/>
        /// 
        /// Do exactly as <see cref="Emplace"/>, 
        /// but after possibly extra allocation.
        /// </summary>
        public int EmplaceBack(in I raw)
        {
            S? s;
            int r = ItoS(in raw, out s);

            if(r != States.OK && (r & States.DONE_HOWEV) == 0)
            {
                return r;
            }

            if(s == null)
            {
                return States.PTR_IS_NULL;
            }

            list.Add(s);
            return States.OK;
        }

        /// <summary>
        /// Instantiates the new object as <see cref="O"/>
        /// and copies to <paramref name="retval"/> if non-null.
        /// </summary>
        /// <param name="index">
        /// Index for <see cref="list"/>. <br/><br/>
        /// 
        /// Given the value is greater than <see cref="Length"/>,
        /// It would cause to return <see cref="States.WRONG_OPERATION"/>.
        /// </param>
        /// <param name="retval">
        /// is a buffer for new instance. <br/><br/>
        /// 
        /// Note that <c>null</c> could be there on execution of <see cref="StoO"/>. <br/><br/>
        /// 
        /// See <see cref="StoO"/>.
        /// </param>
        /// <returns>
        /// <see cref="States"/>
        /// </returns>
        /// <exception cref="States.WRONG_OPERATION"/>
        public int GetSource(int index, out O? retval)
        {
            retval = default;

            if (index >= list.Count)
            {
                return States.WRONG_OPERATION;
            }

            S s = list[index];
            return StoO(in s, out retval);
        }

        #endregion
    }
}
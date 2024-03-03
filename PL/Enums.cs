using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    /// <summary>
    /// Represents a collection of EngineerExperience values.
    /// </summary>
    internal class ExperienceCollection : IEnumerable
    {
        // Static readonly field containing the EngineerExperience values
        static readonly IEnumerable<BO.EngineerExperience> s_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;


        /// <summary>
        /// Returns an enumerator that iterates through the EngineerExperience values.
        /// </summary>
        /// <returns>An enumerator for the EngineerExperience values.</returns>
        public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
    }

    internal class StatusCollection : IEnumerable
    {
        // Static readonly field containing the EngineerExperience values
        static readonly IEnumerable<BO.Status> s_nums =
    (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;


        /// <summary>
        /// Returns an enumerator that iterates through the EngineerExperience values.
        /// </summary>
        /// <returns>An enumerator for the EngineerExperience values.</returns>
        public IEnumerator GetEnumerator() => s_nums.GetEnumerator();
    }



}

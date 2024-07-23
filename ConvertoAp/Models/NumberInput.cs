namespace ConvertoApi.Models
{
    /// <summary>
    /// Represents the input model for converting numbers to words.
    /// </summary>
    public class NumberInput
    {
        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------------
        /// * Method Description : The number to be converted to words.
        /// ----------------------------------------------------------------------------------------------------------------
        /// * Change Summary
        /// * --------------
        ///   ID                Changed By         Changed On      Change Description
        ///*=======             ==========         ==========      ==================
        ///*RZM-127                Rizam          19-July-2024     The number to be converted to words Model
        ///
        /// </summary>
        public decimal Number { get; set; }
    }
}

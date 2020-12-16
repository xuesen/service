namespace IIMes.Services.Runtime.Model.Process
{
    public interface ICollectionSpecItemValue
    {
        /// <summary>
        /// terminal_part.Id
        /// </summary>
        /// <value></value>
        int Id { get; set; }

        /// <summary>
        /// terminal_part.itemId === bop_process_bom.id
        /// </summary>
        /// <value></value>
        int ItemId { get; set; }

        /// <summary>
        /// part.id == terminal_part.scan_part_id
        /// </summary>
        /// <value></value>
        int CandidateId { get; set; }

        /// <summary>
        /// terminal_part.scanno
        /// </summary>
        /// <value></value>
        string Value { get; set; }
        
        /// <summary>
        /// terminal_part.sequence
        /// </summary>
        /// <value></value>
        int Sequence { get; set; }

        int Amount { get; set; }

        int LeftAmount { get; set; }
    }
}


namespace DTOs
{
    public partial class CompoundVariableDTO
    {
        public short CompoundVariableExpressionID { get; set; }

        public short VariableID { get; set; }
        public short? Order { get; set; }
        public string MathOperator { get; set; }
        public double? StaticValue { get; set; }
        public bool InitialBrackets { get; set; }
        public bool FinalBrackets { get; set; }

        public short? AassociateVariableID { get; set; }

        public virtual VariableDTO Variable { get; set; }
        public virtual VariableDTO AassociateVariable { get; set; }
    }
}

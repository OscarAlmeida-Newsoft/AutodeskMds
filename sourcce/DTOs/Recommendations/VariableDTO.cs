namespace DTOs
{
    public class VariableDTO
    {
        public short VariableID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short Type { get; set; }
        public bool? SelectAllFamilies { get; set; }
        public bool? SelectAllProducts { get; set; }
        public string Selector { get; set; }
        public string Field { get; set; }
        public bool? IsCorporate { get; set; }
        public bool? IsCommercial { get; set; }
        public bool? IsSupported { get; set; }
        public bool? IsMathExpression { get; set; }
        public string MathExpression { get; set; }
        public string CustomerVariable { get; set; }
    }
}

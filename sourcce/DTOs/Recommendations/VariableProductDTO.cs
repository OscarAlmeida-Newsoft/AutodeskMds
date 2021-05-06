namespace DTOs
{
    public class VariableProductDTO
    {
        public short VariableProductID { get; set; }
        public short VariableID { get; set; }
        public short ProductID { get; set; }

        public VariableDTO Variable { get; set; }
        public ProductDTO Product { get; set; }
    }
}

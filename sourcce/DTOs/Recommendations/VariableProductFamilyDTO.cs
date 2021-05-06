namespace DTOs
{
    public class VariableProductFamilyDTO
    {
        public short VariableProductFamilyID { get; set; }
        public short VariableID { get; set; }
        public byte ProductFamilyID { get; set; }

        public VariableDTO Variable { get; set; }
        public ProductFamilyDTO ProductFamily { get; set; }
    }
}

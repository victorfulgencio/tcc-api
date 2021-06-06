namespace tcc_back.Dtos
{
    public class AreaDto
    {
        public string SectorCode { get; set; }
        public string Neighborhood { get; set; }
        public string SectorType { get; set; }

        public AreaDto(string code, string neighborhood, string sectorType)
        {
            this.Neighborhood = neighborhood;
            this.SectorCode = code;
            this.SectorType = sectorType;
        }

    }
}
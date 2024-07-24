namespace pokedex_mvc.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<string?> Tipos { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public List<string?> Movimientos { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Satk { get; set; }
        public int Sdef { get; set; }
        public int Spd { get; set; }
    }
}

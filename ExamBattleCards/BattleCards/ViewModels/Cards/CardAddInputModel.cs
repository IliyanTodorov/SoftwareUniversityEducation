namespace BattleCards.ViewModels.Cards
{
    public class CardAddInputModel // TODO: Check if gets data from input model
    {
        public string Name { get; set; }

        public string ImageURL { get; set; }

        public string Keyword { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        public string Description { get; set; }
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class CardManager
{
    private System.Random random = new();
    private readonly ICardRepository cardRepository;

    public CardManager(ICardRepository cardRepository)
    {
        this.cardRepository = cardRepository;
    }

    public Card GetRandomCard()
    {
        var sum = cardRepository.GetDropWeightSum();

        var n = random.Next(sum);

        var limit = 0;

        for (int i = 0; i < cardRepository.CardCount; i++)
        {
            var card = cardRepository.Get(i);
            limit += card.dropWeight;
            if (n < limit)
            {
                return card;
            }
        }
        throw new InvalidOperationException("");
    }

    //����� ������� ���� ��������� �������� �� �������� ������ c ������ � ���� ���������
    public Card GetRandomCardFromList(List<Card> cardList)
    {
        //GetDropWeightSum �� cardsList
        var sum = cardList.Sum(x => x.dropWeight);

        var n = random.Next(sum);

        var limit = 0;

        //���� ��������� �������� �� cardsList
        for (int i = 0; i < cardList.Count; i++)
        {
            var card = cardList[i];

            limit += card.dropWeight;
            if (n < limit)
            {
                return card;
            }
        }
        throw new InvalidOperationException("");
    }

    /// <summary>
    /// Return a deck of certain size
    /// </summary>
    /// <param name="numberOfCards">Size of deck</param>
    /// <returns>List of Cards</returns>
    public List<Card> GetDeck(int numberOfCards)
    {
        List<Card> cardsList = new();
        //�������� ������ �������� cards � ���� cardsList
        for (int i = 0; i < cardRepository.CardCount; i++)
        {
            var card = cardRepository.Get(i);
            cardsList.Add(card);
        }

        //�������������� ���� ��� ����������� ������
        List<Card> selectedCards = new();

        for (int i = 0; i < numberOfCards; i++)
        {
            var card = GetRandomCardFromList(cardsList);
            selectedCards.Add(card);
            cardsList.Remove(card);
        }
        return selectedCards;
    }
}

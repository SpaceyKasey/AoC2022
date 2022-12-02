namespace AoC;

public class DayTwoSolver : ISolver
{
    private const string ElfRock = "A";
    private const string ElfPaper = "B";
    private const string ElfScissors = "C";

    private const string MeRock = "X";
    private const string MePaper = "Y";
    private const string MeScissors = "Z";

    private const string MeLoose = "X";
    private const string MeDraw = "Y";
    private const string MeWin = "Z";

    private const int RockValue = 1;
    private const int PaperValue = 2;
    private const int ScissorsValue = 3;

    private const int WinValue = 6;
    private const int DrawValue = 3;
    private const int LossValue = 0;

    enum Play
    {
        Rock,
        Paper,
        Scissors
    }

    public int Day => 2;

    public string[] Solve(string[] input, int part = 1)
    {

        var score = 00;
        foreach (var round in input)
        {
            var parts = round.Split(' ');
            var elf = parts[0];
            var me = parts[1];

            Play elfPlay = ResolveElf(elf);
            Play mePlay = part == 1 ? ResolveMe(me) : ResolveMe2(me, elfPlay);


            if (elfPlay == mePlay)
            {
                //Draw
                score += DrawValue;
            }
            else if (mePlay == Play.Paper && elfPlay == Play.Rock
                     || mePlay == Play.Rock && elfPlay == Play.Scissors
                     || mePlay == Play.Scissors && elfPlay == Play.Paper)
            {
                // Win
                score += WinValue;
            }
            else
            {
                //loss
                score += LossValue;
            }

            switch (mePlay)
            {
                case Play.Rock:
                    score += RockValue;
                    break;
                case Play.Paper:
                    score += PaperValue;
                    break;
                case Play.Scissors:
                    score += ScissorsValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }



            Play ResolveMe(string s)
            {
                switch (s)
                {
                    case MeRock:
                        return Play.Rock;
                    case MePaper:
                        return Play.Paper;
                    case MeScissors:
                        return Play.Scissors;
                    default:
                        throw new ArgumentOutOfRangeException("Invalid Me Input");
                }
            }

            Play ResolveMe2(string me, Play elf)
            {
                switch (me)
                {
                    case MeWin:
                        if (elfPlay == Play.Paper)
                        {
                            return Play.Scissors;
                        }

                        if (elfPlay == Play.Scissors)
                        {
                            return Play.Rock;
                        }

                        if (elfPlay == Play.Rock)
                        {
                            return Play.Paper;
                        }

                        throw new ArgumentOutOfRangeException();

                    case MeLoose:
                        if (elfPlay == Play.Paper)
                        {
                            return Play.Rock;
                        }

                        if (elfPlay == Play.Scissors)
                        {
                            return Play.Paper;
                        }

                        if (elfPlay == Play.Rock)
                        {
                            return Play.Scissors;
                        }

                        throw new ArgumentOutOfRangeException();

                    case MeDraw:
                        return elf;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Play ResolveElf(string s)
            {
                switch (s)
                {
                    case ElfRock:
                        return Play.Rock;
                    case ElfPaper:
                        return Play.Paper;
                    case ElfScissors:
                        return Play.Scissors;
                    default:
                        throw new ArgumentOutOfRangeException("Invalid Elf Input");
                }
            }

        }

        return new[] { score.ToString() };
    }
}
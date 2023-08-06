using sapir_c23_dn_course_gil_and_david.Ex02_01;

public class Program
{
    public static void Main()
    {
        GameBoard.WelcomePlayer();
        uint maxGuessesCount = GameBoard.GetValidGuessesCount();
        GameBoard game = new GameBoard(maxGuessesCount);
        game.PrintCurrentState();
        char[] guess = GameBoard.GetValidGuess();
        game.AddTurn(new Turn(guess));
        game.AddTurn(new Turn(guess));

        game.PrintCurrentState();
    }
}
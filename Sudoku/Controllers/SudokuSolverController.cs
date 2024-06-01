using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Sudoku.Controllers
{
    [Route("api/SolveSudoku")]
    [ApiController]
    public class SudokuSolverController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> SolveSudoku(string jsonInput)
        {
            Board boardInput = new Board();
            try
            {
                boardInput = JsonSerializer.Deserialize<Board>(jsonInput);
            }
            catch(JsonException)
            {
                return BadRequest();
            }
            
            SudokuSolver solver = new SudokuSolver();
            if(!solver.ValidateBoard(boardInput.board))
            {
                return BadRequest();
            }

            boardInput.board = solver.SolveBoard(boardInput.board);
            if(boardInput.board == null)
            {
                return BadRequest();
            }

            string jsonResponse = JsonSerializer.Serialize(boardInput);
            return jsonResponse;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Assessment.Ozow.Conway
{
    [ExcludeFromCodeCoverage]
    public static class Boards
    {
        public static readonly Board BLINKER_GEN_0= new Board(new bool[,] {
            {false, false, false, false, false},
            {false, false, true , false, false},
            {false, false, true , false, false},
            {false, false, true , false, false},
            {false, false, false, false, false}
        });

        public static readonly Board BLINKER_GEN_1 = new Board(new bool[,] {
            {false, false, false, false, false},
            {false, false, false, false, false},
            {false, true , true , true , false},
            {false, false, false, false, false},
            {false, false, false, false, false}
        });
    }
}

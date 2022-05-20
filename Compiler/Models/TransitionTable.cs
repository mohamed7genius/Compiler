namespace Compiler.Models
{
    public static class TransitionTable
    {

        static int[,] stateTransitions = new int[99, 127];

        public static int[,] init()
        {

            for (int row = 0; row < 99; row++)
            {
                for (int column = 0; column < 127; column++)
                {
                    stateTransitions[row, column] = -1;
                }
            }

            stateTransitions[(int)State.A, '!'] = (int)State.B;
            stateTransitions[(int)State.A, '\"'] = (int)State.C;
            stateTransitions[(int)State.A, '+'] = (int)State.D;
            stateTransitions[(int)State.A, '&'] = (int)State.E;
            stateTransitions[(int)State.A, '\''] = (int)State.F;
            stateTransitions[(int)State.A, '-'] = (int)State.G;
            stateTransitions[(int)State.A, '/'] = (int)State.H;
            stateTransitions[(int)State.A, '<'] = (int)State.I;
            stateTransitions[(int)State.A, '='] = (int)State.J;
            stateTransitions[(int)State.A, '>'] = (int)State.K;
            stateTransitions[(int)State.A, '*'] = (int)State.L;
            stateTransitions[(int)State.A, '('] = (int)State.M;
            stateTransitions[(int)State.A, 'C'] = (int)State.N;
            stateTransitions[(int)State.A, 'D'] = (int)State.O;
            stateTransitions[(int)State.A, 'E'] = (int)State.P;
            stateTransitions[(int)State.A, 'I'] = (int)State.Q;
            stateTransitions[(int)State.A, 'L'] = (int)State.R;
            stateTransitions[(int)State.A, ')'] = (int)State.S;
            stateTransitions[(int)State.A, 'S'] = (int)State.T;
            stateTransitions[(int)State.A, 'T'] = (int)State.U;
            stateTransitions[(int)State.A, 'W'] = (int)State.V;
            stateTransitions[(int)State.A, '['] = (int)State.W;
            stateTransitions[(int)State.A, '|'] = (int)State.X;
            stateTransitions[(int)State.A, ']'] = (int)State.Y;
            stateTransitions[(int)State.A, '{'] = (int)State.Z;
            stateTransitions[(int)State.A, '}'] = (int)State.AA;
            stateTransitions[(int)State.A, '~'] = (int)State.AB;
            stateTransitions[(int)State.B, '='] = (int)State.AC;
            stateTransitions[(int)State.E, '&'] = (int)State.AD;
            stateTransitions[(int)State.G, '>'] = (int)State.AE;
            stateTransitions[(int)State.I, '='] = (int)State.AF;
            stateTransitions[(int)State.J, '='] = (int)State.AG;
            stateTransitions[(int)State.K, '='] = (int)State.AH;
            stateTransitions[(int)State.N, 'h'] = (int)State.AI;
            stateTransitions[(int)State.O, 'D'] = (int)State.AJ;
            stateTransitions[(int)State.P, 'l'] = (int)State.AK;
            stateTransitions[(int)State.Q, 'f'] = (int)State.AL;
            stateTransitions[(int)State.Q, 'n'] = (int)State.AM;
            stateTransitions[(int)State.Q, 'o'] = (int)State.AN;
            stateTransitions[(int)State.Q, 't'] = (int)State.AO;
            stateTransitions[(int)State.R, 'o'] = (int)State.AP;
            stateTransitions[(int)State.T, 'I'] = (int)State.AQ;
            stateTransitions[(int)State.T, 't'] = (int)State.AR;
            stateTransitions[(int)State.U, 'u'] = (int)State.AS;
            stateTransitions[(int)State.V, 'o'] = (int)State.AT;
            stateTransitions[(int)State.X, '|'] = (int)State.AU;
            stateTransitions[(int)State.AI, 'a'] = (int)State.AV;
            stateTransitions[(int)State.AI, 'l'] = (int)State.AW;
            stateTransitions[(int)State.AJ, 'D'] = (int)State.AJ;
            stateTransitions[(int)State.AK, 's'] = (int)State.AX;
            stateTransitions[(int)State.AM, 'c'] = (int)State.AY;
            stateTransitions[(int)State.AN, 'w'] = (int)State.AZ;
            stateTransitions[(int)State.AO, 'e'] = (int)State.BA;
            stateTransitions[(int)State.AP, 'l'] = (int)State.BB;
            stateTransitions[(int)State.AP, 'o'] = (int)State.BC;
            stateTransitions[(int)State.AQ, 'o'] = (int)State.BD;
            stateTransitions[(int)State.AR, 'o'] = (int)State.BE;
            stateTransitions[(int)State.AS, 'r'] = (int)State.BF;
            stateTransitions[(int)State.AT, 'r'] = (int)State.BG;
            stateTransitions[(int)State.AV, 'i'] = (int)State.BH;
            stateTransitions[(int)State.AW, 'o'] = (int)State.BI;
            stateTransitions[(int)State.AX, 'e'] = (int)State.BJ;
            stateTransitions[(int)State.AY, 'l'] = (int)State.BK;
            stateTransitions[(int)State.AZ, 'f'] = (int)State.BL;
            stateTransitions[(int)State.BA, 'r'] = (int)State.BM;
            stateTransitions[(int)State.BB, 'i'] = (int)State.BN;
            stateTransitions[(int)State.BC, 'p'] = (int)State.BO;
            stateTransitions[(int)State.BD, 'w'] = (int)State.BP;
            stateTransitions[(int)State.BE, 'p'] = (int)State.BQ;
            stateTransitions[(int)State.BF, 'n'] = (int)State.BR;
            stateTransitions[(int)State.BG, 't'] = (int)State.BS;
            stateTransitions[(int)State.BH, 'n'] = (int)State.BT;
            stateTransitions[(int)State.BK, 'u'] = (int)State.BU;
            stateTransitions[(int)State.BM, 'a'] = (int)State.BV;
            stateTransitions[(int)State.BO, 'w'] = (int)State.BW;
            stateTransitions[(int)State.BP, 'f'] = (int)State.BX;
            stateTransitions[(int)State.BR, 'b'] = (int)State.BY;
            stateTransitions[(int)State.BS, 'h'] = (int)State.BZ;
            stateTransitions[(int)State.BU, 'd'] = (int)State.CA;
            stateTransitions[(int)State.BY, 'a'] = (int)State.CD;
            stateTransitions[(int)State.BV, 't'] = (int)State.CB;
            stateTransitions[(int)State.BW, 'h'] = (int)State.CC;
            stateTransitions[(int)State.BY, 'a'] = (int)State.CD;
            stateTransitions[(int)State.BZ, 'l'] = (int)State.CE;
            stateTransitions[(int)State.CA, 'e'] = (int)State.CF;
            stateTransitions[(int)State.CB, 'e'] = (int)State.CG;
            stateTransitions[(int)State.CC, 'e'] = (int)State.CH;
            stateTransitions[(int)State.CD, 'c'] = (int)State.CI;
            stateTransitions[(int)State.CE, 'e'] = (int)State.CJ;
            stateTransitions[(int)State.CG, 'w'] = (int)State.CK;
            stateTransitions[(int)State.CH, 'n'] = (int)State.CL;
            stateTransitions[(int)State.CI, 'k'] = (int)State.CM;
            stateTransitions[(int)State.CJ, 's'] = (int)State.CN;
            stateTransitions[(int)State.CK, 'h'] = (int)State.CO;
            stateTransitions[(int)State.CN, 's'] = (int)State.CP;
            stateTransitions[(int)State.CO, 'e'] = (int)State.CQ;
            stateTransitions[(int)State.CQ, 'n'] = (int)State.CR;

            return stateTransitions;

        }
    }
}

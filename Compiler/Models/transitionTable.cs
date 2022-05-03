using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compiler.Models
{

    public static class transitionTable 
    {


        static int[,] stateTransitions= new int[99,127];

        public  static int[,] init()
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
            stateTransitions[(int)State.T, 'l'] = (int)State.AR;
            stateTransitions[(int)State.T, 't'] = (int)State.AS;
            stateTransitions[(int)State.U, 'u'] = (int)State.AT;
            stateTransitions[(int)State.V, 'o'] = (int)State.AU;
            stateTransitions[(int)State.X, '|'] = (int)State.AV;
            stateTransitions[(int)State.AI, 'a'] = (int)State.AW;
            stateTransitions[(int)State.AI, 'l'] = (int)State.AX;
            stateTransitions[(int)State.AJ, 'D'] = (int)State.AJ;
            stateTransitions[(int)State.AK, 's'] = (int)State.AY;
            stateTransitions[(int)State.AM, 'c'] = (int)State.AZ;
            stateTransitions[(int)State.AN, 'w'] = (int)State.BA;
            stateTransitions[(int)State.AO, 'e'] = (int)State.BB;
            stateTransitions[(int)State.AP, 'l'] = (int)State.BC;
            stateTransitions[(int)State.AP, 'o'] = (int)State.BD;
            stateTransitions[(int)State.AQ, 'o'] = (int)State.BE;
            stateTransitions[(int)State.AR, 'o'] = (int)State.BF;
            stateTransitions[(int)State.AS, 'o'] = (int)State.BG;
            stateTransitions[(int)State.AT, 'r'] = (int)State.BH;
            stateTransitions[(int)State.AU, 'r'] = (int)State.BI;
            stateTransitions[(int)State.AW, 'i'] = (int)State.BJ;
            stateTransitions[(int)State.AX, 'o'] = (int)State.BK;
            stateTransitions[(int)State.AY, 'e'] = (int)State.BL;
            stateTransitions[(int)State.AZ, 'l'] = (int)State.BM;
            stateTransitions[(int)State.BA, 'f'] = (int)State.BN;
            stateTransitions[(int)State.BB, 'r'] = (int)State.BO;
            stateTransitions[(int)State.BC, 'i'] = (int)State.BP;
            stateTransitions[(int)State.BD, 'p'] = (int)State.BQ;
            stateTransitions[(int)State.BE, 'w'] = (int)State.BR;
            stateTransitions[(int)State.BF, 'w'] = (int)State.BS;
            stateTransitions[(int)State.BG, 'p'] = (int)State.BT;
            stateTransitions[(int)State.BH, 'n'] = (int)State.BU;
            stateTransitions[(int)State.BI, 't'] = (int)State.BV;
            stateTransitions[(int)State.BJ, 'n'] = (int)State.BW;
            stateTransitions[(int)State.BM, 'u'] = (int)State.BX;
            stateTransitions[(int)State.BO, 'a'] = (int)State.BY;
            stateTransitions[(int)State.BQ, 'w'] = (int)State.BZ;
            stateTransitions[(int)State.BR, 'f'] = (int)State.CA;
            stateTransitions[(int)State.BU, 'b'] = (int)State.CB;
            stateTransitions[(int)State.BV, 'h'] = (int)State.CC;
            stateTransitions[(int)State.BX, 'd'] = (int)State.CD;
            stateTransitions[(int)State.BY, 't'] = (int)State.CE;
            stateTransitions[(int)State.BZ, 'h'] = (int)State.CF;
            stateTransitions[(int)State.CB, 'a'] = (int)State.CG;
            stateTransitions[(int)State.CC, 'l'] = (int)State.CH;
            stateTransitions[(int)State.CD, 'e'] = (int)State.CI;
            stateTransitions[(int)State.CE, 'e'] = (int)State.CJ;
            stateTransitions[(int)State.CF, 'e'] = (int)State.CK;
            stateTransitions[(int)State.CG, 'c'] = (int)State.CL;
            stateTransitions[(int)State.CH, 'e'] = (int)State.CM;
            stateTransitions[(int)State.CJ, 'w'] = (int)State.CN;
            stateTransitions[(int)State.CK, 'n'] = (int)State.CO;
            stateTransitions[(int)State.CL, 'k'] = (int)State.CP;
            stateTransitions[(int)State.CM, 's'] = (int)State.CQ;
            stateTransitions[(int)State.CN, 'h'] = (int)State.CR;
            stateTransitions[(int)State.CQ, 's'] = (int)State.CS;
            stateTransitions[(int)State.CR, 'e'] = (int)State.CT;
            stateTransitions[(int)State.CT, 'n'] = (int)State.CU;
            return stateTransitions;


        }



    }
}
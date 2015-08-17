using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SharedGameClasses {

    /// <summary>
    /// Represents a die (or "dice") that can be rolled. 
    /// By default, it has six sides, but dice with other numbers of sides are allowed.
    /// </summary>
    public class Die {
        private const int MIN_FACES = 4;
        private const int DEFAULT_FACE_VALUE = 1;
        private const int SIX_SIDED = 6;

        // number of sides on the die
        private int numOfFaces;
        public int NumOfFaces {
            get {
                return numOfFaces;
            }
        }
        
        // current value showing on the die
        private int faceValue;
        public int FaceValue {
            get {
                return faceValue;
            }
        }
        
        private int initialFaceValue; //use only in Reset()
    
        private static Random random = new Random();
    

        //-----------------------------------------------------------------
        // Parameterless Constructor
        // defaults to a six-sided die with a default initial face value.
        // Pre:  none
        // Post: the die is initialised.
        //-----------------------------------------------------------------
        public Die() {
            numOfFaces = SIX_SIDED;
            faceValue = DEFAULT_FACE_VALUE;
            initialFaceValue = faceValue;
        } 

        //-----------------------------------------------------------------
        // Explicitly sets the size of the die. Defaults to a size of
        // six if the parameter is invalid.  Face value is randomly chosen
        // Pre:  none
        // Post: the die is initialised.
        //-----------------------------------------------------------------
        public Die(int faces){
           
            if (faces < MIN_FACES) {
                numOfFaces = SIX_SIDED;
            } else {
                numOfFaces = faces;
            }

            faceValue = Roll();
            initialFaceValue = faceValue;
        }

       //-----------------------------------------------------------------
       // Rolls the die and returns the result.
        // Pre:  none
        // Post: a random side of the die has been selected.
        //-----------------------------------------------------------------
       public int Roll() {
           faceValue =  random.Next(NumOfFaces) + 1;
           return FaceValue;
       }
     
     
       //-----------------------------------------------------------------
       // Resets the die face value to its initial value.
       // Pre:  none
       // Post: the die has its initialFaceValue
       //-----------------------------------------------------------------
        public void Reset() {
            faceValue = initialFaceValue;
        }
  
        //----------------------------------------------------------------
        // Returns a String representation of the dice's attributes.
        // Pre:  none
        // Post: the returned string contains the current face and the number of faces.
        //----------------------------------------------------------------
        public override string ToString() {
            string str = "current face    = " + faceValue + "\n" +
                         "number of faces = " + numOfFaces + "\n" ;
            return str;
       }
    }
}

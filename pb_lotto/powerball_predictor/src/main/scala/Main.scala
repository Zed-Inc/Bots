import util.control.Breaks._
import java.lang.Integer

class CalculateNumberScores() {
  var occurences: Map[Int, Int] = Map()
  var scores: Map[Int, Float] = Map()

  // adds a new number to the score map
  def addNewNumbers(numbers: Array[String]) = {
    // drop the date col
    val cleaned = numbers.drop(1)

    for (col <- cleaned) {
      // skip over the header of the csv file
      // or skip over the date which is the first item of the row
      val num = col.toInt
      // check if the map doesn't already hold our number
      if (!occurences.contains(num)) {
        // add it to the map
        occurences += (num -> 1)
      } else {
        // update itself
        occurences = occurences.updated(num, occurences.apply(num) + 1)
      }

    }

  }
  // print out the map
  def displayOccurences() = {
    occurences.foreach { i =>
      println(i)
    }
  }

  // calculates the number with the higest occurences and returns a map that is the score for eacg number
  def calculateScore(): Map[Int, Float] = {
    var common: (Int, Int) = (0, 0)
    var map: Map[Int, Float] = Map()
    occurences.foreach { i =>
      if (i._2 > common._2) {
        common = i
      }
    }
    val div = common._2.toFloat
    // loop through the occurences and a new item to the map for each number
    // dividing each numbers occurence by the highest number of occurences
    occurences.foreach { i =>
      map += (i._1 -> (i._2.toFloat / div))
    }
    return map
  }
}

class GenerateLottoNumbers(
    val numberScores: Map[Int, Float]
) {
  // generates the numbers !
  def generate() = {
    // 7 normal numbers from 1->35
    // and a powerball number from 1-> 20

    // the 7 numbers, the powerball number and the score for it
    var bestCombo: (Array[Int], Int, Float) = (new Array[Int](0), 0, 0.0f)
    var combosChecked = 0
    for (q <- 1 to 35) {
      for (w <- 1 to 35) {
        for (e <- 1 to 35) {
          for (r <- 1 to 35) {
            for (t <- 1 to 35) {
              for (y <- 1 to 35) {
                for (u <- 1 to 35) {
                  val qScore = numberScores.apply(q)
                  val wScore = numberScores.apply(w)
                  val eScore = numberScores.apply(e)
                  val rScore = numberScores.apply(r)
                  val tScore = numberScores.apply(t)
                  val yScore = numberScores.apply(y)
                  val uScore = numberScores.apply(u)
                  // total score of the 7 chosen numbers
                  val totalScore =
                    qScore + wScore + eScore + rScore + tScore + yScore + uScore
                  for (p <- 1 to 20) {
                    val withPowerball = totalScore + numberScores.apply(p)
                    if (withPowerball > bestCombo._3) {
                      bestCombo = (
                        Array(
                          q,
                          w,
                          e,
                          r,
                          t,
                          y,
                          u
                        ),
                        p,
                        withPowerball
                      )
                    }
                    combosChecked += 1
                  }
                }
              }
            }
          }
        }
      }
    }
    println("Combos checked: " + combosChecked)
    println("best number set: " + bestCombo._1)
    println("best powerball: " + bestCombo._2)
    println("with a score of: " + bestCombo._3)
    println()

  }
}

object Main extends App {
  println("\n\n")
  // open the file for reading
  val bufferedSource = io.Source.fromFile("data/Powerball_Results.csv")

  val numScores = new CalculateNumberScores()

  var i = 0
  // read the lines from the file and loop through them
  for (line <- bufferedSource.getLines()) {
    breakable {
      if (i == 0) {
        i += 1
        break
      } else {
        // split the line on the ',' and then trim whitespace
        val cols = line.split(",").map(_.trim())
        numScores.addNewNumbers(cols)

      }
    }
  }

  println("file loaded")
  // close the file
  bufferedSource.close()

  // numScores.displayOccurences()
  val scores: Map[Int, Float] = numScores.calculateScore()
  // print the number and their score
  scores.foreach(i => println(s"${i._1}, score of  ${i._2}"))

  val genny = new GenerateLottoNumbers(scores)
  genny.generate()

}

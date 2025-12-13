import java.io.File

val sampleInput = File("puzzles/2025/Day 2/Sample.txt").readText()
val puzzleInput = File("puzzles/2025/Day 2/Input.txt").readText()

val simpleValidator = SimpleIdValidator()
val simpleSampleSum = sumInvalidIds(sampleInput, simpleValidator)
val simplePuzzleSum = sumInvalidIds(puzzleInput, simpleValidator)
println("Simple Validator: Sample ($simpleSampleSum), Puzzle ($simplePuzzleSum)")

val sillyValidator = SillyIdValidator()
val sillySampleSum = sumInvalidIds(sampleInput, sillyValidator)
val sillyPuzzleSum = sumInvalidIds(puzzleInput, sillyValidator)
println("Silly Validator: Sample ($sillySampleSum), Puzzle ($sillyPuzzleSum)")

fun sumInvalidIds(input: String, validator: IdValidator): Long {
	return input.split(",").sumOf { sumInvalidIdsInRange(it.trim(), validator) }
}

fun sumInvalidIdsInRange(range: String, validator: IdValidator): Long {
	val (firstId, lastId) = range.split("-").map { it.toLong() }
	return (firstId..lastId).filter { !validator.isValid(it) }.sum()
}

interface IdValidator {
	fun isValid(id: Long): Boolean
}

class SimpleIdValidator : IdValidator {
	override fun isValid(id: Long): Boolean {
		val inputAsString = id.toString()
		if (inputAsString.length % 2 != 0)
			return true;

		return inputAsString.chunked(inputAsString.length / 2).let { (a, b) -> a != b }
	}
}

class SillyIdValidator : IdValidator {
	override fun isValid(id: Long): Boolean {
		val inputAsString = id.toString()
		for (size in 1..(inputAsString.length / 2)) {
			if (inputAsString.length % size != 0)
				continue

			val chunk = inputAsString.substring(0, size)
			val repeated = chunk.repeat(inputAsString.length / size)
			if (repeated == inputAsString)
				return false
		}

		return true
	}
}
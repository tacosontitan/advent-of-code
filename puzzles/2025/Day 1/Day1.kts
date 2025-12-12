import java.io.File

val sampleInput = File("puzzles/2025/Day 1/Sample.txt").readText()
checkPassword<FullStopCalculator>(sampleInput, "3")
checkPassword<StepByStepCalculator>(sampleInput, "6")

val puzzleInput = File("puzzles/2025/Day 1/Input.txt").readText()
checkPassword<FullStopCalculator>(puzzleInput, "1120")
checkPassword<StepByStepCalculator>(puzzleInput, "6554")

inline fun<reified T : PasswordCalculator> checkPassword(input: String, expectedPassword: String) {
	val calculator = T::class.java.getDeclaredConstructor().newInstance()
	val actualPassword = calculator.calculate(input)
	check(actualPassword == expectedPassword) { "Test failed: Expected '$expectedPassword', got '$actualPassword'" }
	println("Test passed: Password is '$actualPassword'")
}

abstract class PasswordCalculator {
	protected var password = 0
	protected var tumblerPosition = 50

	public fun calculate(input: String): String {
		val rotations = input.lines()
		for (rotation in rotations) {
			val direction = rotation[0]
			val distance = rotation.substring(1).toInt()
			processRotation(direction, distance)
		}

		return password.toString()
	}

	protected abstract fun processRotation(direction: Char, distance: Int)
	protected fun calculateStoppingPosition(direction: Char, distance: Int): Int {
		return when (direction) {
			'L' -> (tumblerPosition - distance + 100) % 100
			'R' -> (tumblerPosition + distance) % 100
			else -> tumblerPosition
		}
	}
}

class FullStopCalculator : PasswordCalculator() {
	protected override fun processRotation(direction: Char, distance: Int) {
		tumblerPosition = calculateStoppingPosition(direction, distance)
		if (tumblerPosition == 0) {
			password += 1
		}
	}
}

class StepByStepCalculator : PasswordCalculator() {
	protected override fun processRotation(direction: Char, distance: Int) {
		for (step in 1..distance) {
			tumblerPosition = calculateStoppingPosition(direction, 1)
			if (tumblerPosition == 0) {
				password += 1
			}
		}
	}
}
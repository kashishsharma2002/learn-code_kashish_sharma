import random
def is_valid_numeric_input(user_input):
    if user_input.isdigit() and 1<= int(user_input) <=100:
        return True
    else:
        return False

def main():
    number_to_guess=random.randint(1,100)
    has_guessed=False
    user_guess=input("Guess a number between 1 and 100:")
    guess_counter=0
    while not has_guessed:
        if not is_valid_numeric_input(user_guess):
            user_guess=input("I wont count this one Please enter a number between 1 to 100")
            continue
        else:
            guess_counter+=1
            user_guess=int(user_guess)
        if user_guess<number_to_guess:
            user_guess=input("Too low. Guess again")
        elif user_guess>number_to_guess:
            user_guess=input("Too High. Guess again")
        else:
            print("You guessed it in",guess_counter,"guesses!")
            has_guessed=True
main()
import random
def roll_the_dice(dice_sides):
    number=random.randint(1, dice_sides)
    return number


def main():
    number_of_sides=6
    keep_rolling =True
    while keep_rolling:
        user_choice=input("Ready to roll? Enter Q to Quit")
        if user_choice.lower() !="q":
            number=roll_the_dice(number_of_sides)
            print("You have rolled a",number)
        else:
            keep_rolling=False
main()
def get_armstrong_sum(user_input):
    armstrong_sum = 0
    digit_count = 0
    temporary_number = user_input

    while temporary_number > 0:
        digit_count = digit_count + 1
        temporary_number = temporary_number // 10

    temporary_number = user_input
    for index in range(1, digit_count + 1):
        digit = temporary_number % 10
        armstrong_sum = armstrong_sum + (digit ** digit_count)
        temporary_number //= 10
    return armstrong_sum

user_input = int(input("\nPlease Enter the Number to Check for Armstrong: "))

if (user_input == get_armstrong_sum(user_input)):
    print("\n %d is Armstrong Number.\n" % user_input)
else:
    print("\n %d is Not a Armstrong Number.\n" % user_input)
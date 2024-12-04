#include <string>

// Used _CRT_SECURE_NO_WARNINGS for strncpy

#define MAX_PARAMS 4
#define MAX_PARAM_SIZE 20
#define DELIMITER	" "
#define NUMBERS "+-0123456789"
#define ALPHABET "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
#define MIN(x, y) (x < y) ? x : y

uint32_t c_string_split(char* str, const char* delimiter, char* arguments, uint32_t cnt, uint32_t size)
{
	char* next_token = NULL;
	unsigned int uParam = 0;

	char* next = strtok_s(str, delimiter, &next_token);
	while (next != NULL)
	{
		if (uParam < cnt)
		{
			uint32_t capacity = MIN(strlen(next), size);
			strncpy(arguments + uParam * size, next, capacity);
		}
		uParam++;
		next = strtok_s(NULL, delimiter, &next_token);
	}
	return uParam;
}

int c_clamp(int number, int min_value, int max_value)
{
	if (number > max_value)
	{
		number = max_value;
	}
	else if (number < min_value)
	{
		number = min_value;
	}
	return number;
}

bool c_is_it_number(const char* str)
{
	if ((NULL != str) && strspn(str, NUMBERS) == strlen(str))
	{
		return true;
	}
	return false;
}

bool c_is_it_string(const char* str)
{
	if ((NULL != str) && strspn(str, ALPHABET) == strlen(str))
	{
		return true;
	}
	return false;
}

bool c_string_check(char* in_str)
{
	printf("\n");
	printf("in_str: %s\n", in_str);
	if (NULL == in_str || 0 == strlen(in_str))
	{
		printf("invalid incoming string\n");
		return false;
	}

	char arguments[MAX_PARAMS][MAX_PARAM_SIZE] = { 0 };

	char* pend = NULL;

	uint32_t nargs = c_string_split(in_str, DELIMITER, (char*)arguments, MAX_PARAMS, MAX_PARAM_SIZE);

	if (nargs > 0)
	{
		if (c_is_it_string(arguments[0]))
		{
			printf("command: %s\n", arguments[0]);
			// check the number of params for certain command
			printf("params : ");
			for (uint32_t i = 1; i < nargs; ++i)
			{
				if (c_is_it_number(arguments[i]))
				{
					int value = strtol(arguments[i], &pend, 10);
					printf(" %d", value);
				}
				else
				{
					printf("invalid param\n");
					return false;
				}
			}
		}
		else
		{
			return false;
			printf("invalid command\n");
		}
	}


	return true;
}

void c_string_parser()
{
	char test0[] = "rgb 10 20 30";
	char test1[] = "rgb 10 ";
	char test2[] = "rgbfff -1050 +1000 2232";
	char test3[] = "xffdrgb 10 100 22";
	char test4[] = "     rgb 1050 1000 2232    ";
	char test5[] = "rgb 10 20 30 123 123";
	char test6[] = "rgsfdsgjhdsfgjhdsgfjhdsgfjhgdsjhfgdsjhfgjhdsfb 10 20 30";

	if (!c_string_check(test0))
	{
		printf("invalid input\n");
	}
	if (!c_string_check(test1))
	{
		printf("invalid input\n");
	}
	if (!c_string_check(test2))
	{
		printf("invalid input\n");
	}
	if (!c_string_check(test3))
	{
		printf("invalid input\n");
	}
	if (!c_string_check(test4))
	{
		printf("invalid input\n");
	}
	if (!c_string_check(test5))
	{
		printf("invalid input\n");
	}
	if (!c_string_check(test6))
	{
		printf("invalid input\n");
	}
}

int main()
{
	c_string_parser();
	return 0;
}
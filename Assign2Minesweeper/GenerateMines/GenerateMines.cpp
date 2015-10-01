#include "GenerateMines.h"

GenerateMines::GenerateMines()
{
}


GenerateMines::~GenerateMines()
{
}

void GenerateMines::generateField(const int x, const int y)
{
	m_mineField.resize(x);
	for (auto& vec : m_mineField)
	{
		vec.resize(y);
	}

	//about 10% mines
	std::uniform_int_distribution<>rand(0, 9);
	std::random_device rd;
	std::mt19937 gen;
	for (auto& i : m_mineField)
	{
		gen.seed(rd());
		for (auto vec : i)
		{
			vec = !static_cast<bool>(rand(gen));
		}
	}
}

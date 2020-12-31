#include "stdafx.h"
#include "CppUnitTest.h"
#include "../TP1 - Session 3/Testeur.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace ProjetSFMLTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(TestCollisionSpheresBetweenPlayerAndLasPlagas)
		{
			Game game;

			Player player(10, 10);
			LasPlagas lasPlagas(10, 10, 0.0f);

			Testeur testeur;		
			Assert::IsTrue(game.TestCollisionSpheresBetweenPlayerAndLasPlagas(lasPlagas));
		}
		TEST_METHOD(TestCollisionSpheresBetweenPlayerAndLasPlagas)
		{
			Game game;

			Player player(10, 10);
			LasPlagas lasPlagas(100, 100, 0.0f);



			Testeur testeur;
			Assert::IsTrue(game.TestCollisionSpheresBetweenPlayerAndLasPlagas(lasPlagas));
		}
		TEST_METHOD(TestCollisionSpheresBetweenPlayerAndLasPlagas)
		{
			Game game;

			Player player(10, 10);
			LasPlagas lasPlagas(200, 200, 0.0f);



			Testeur testeur;
			Assert::IsFalse(game.TestCollisionSpheresBetweenPlayerAndLasPlagas(lasPlagas));
		}

		TEST_METHOD(TestCollisionSpheresBetweenPlayerAndLasPlagas)
		{
			Game game;

			Player player(10, 10);
			LasPlagas lasPlagas(500, 500, 0.0f);

			Testeur testeur;
			Assert::IsFalse(game.TestCollisionSpheresBetweenPlayerAndLasPlagas(lasPlagas));
		}

	};
}
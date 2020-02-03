using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEditor;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace Tests
{
    public class NewTestScriptPlay
    {
        private Player player;
        private TestPlayModeScene scene;
        private bool sceneLoading;

        [OneTimeSetUp]
        public void InitializeTest()
        {
            sceneLoading = true;

            SceneManager.LoadSceneAsync("TestPlayModeScene").completed += _ =>
            {
                Debug.Log("Scene Loaded");
                player = Object.FindObjectOfType<Player>();
                scene = Object.FindObjectOfType<TestPlayModeScene>();
                sceneLoading = false;
            };
        }

        public void ResetPosition()
        {
            player.transform.position = new Vector3(0, -3, 0);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Debug.Log("これからプレイテストを開始します");
        }

        [SetUp]
        public void SetUp()
        {
            Debug.Log("セットアップ");
        }

        [TearDown]
        public void TearDown()
        {
            Debug.Log("ティアダウン");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Debug.Log("これでプレイテストを終了します");
        }

        [UnityTest]
        [Order(-100)]
        public IEnumerator 最初に呼ばれるテストでロード待ち()
        {
            yield return new WaitWhile(() => sceneLoading);
        }

        [UnityTest]
        [Order(-99)]
        public IEnumerator 待機()
        {
            yield return new WaitForSeconds(0.8f);
        }

        [UnityTest]
        [Order(-98)]
        public IEnumerator プレイヤーが存在しているか()
        {
            Assert.IsNotNull(player);

            yield return null;
        }

        [UnityTest]
        [Order(-98)]
        public IEnumerator シーンコントローラーが存在しているか()
        {
            Assert.IsNotNull(scene);

            yield return null;
        }

        [UnityTest]
        public IEnumerator 右に動くか()
        {
            ResetPosition();
            Vector3 org = player.transform.position;
            int waitTime = 50;

            while (waitTime > 0)
            {
                player.Move(Vector3.right, false);
                waitTime--;

                yield return null;
            }

            Vector3 moved = player.transform.position;
            Assert.Greater(moved.x, org.x);
        }

        [UnityTest]
        public IEnumerator 左に動くか()
        {
            ResetPosition();
            Vector3 org = player.transform.position;
            int waitTime = 50;

            while (waitTime > 0)
            {
                player.Move(Vector3.left, false);
                waitTime--;

                yield return null;
            }

            Vector3 moved = player.transform.position;
            Assert.Less(moved.x, org.x);
        }

        [UnityTest]
        public IEnumerator 右にダッシュできるか()
        {
            ResetPosition();
            int waitTime = 50;

            while (waitTime > 0)
            {
                player.Move(Vector3.right, false);
                waitTime--;

                yield return null;
            }

            Transform transform = player.transform;
            Vector3 normal = transform.position;

            ResetPosition();
            waitTime = 50;

            while (waitTime > 0)
            {
                player.Move(Vector3.right, true);
                waitTime--;

                yield return null;
            }

            Vector3 dash = player.transform.position;
            Assert.Less(normal.x, dash.x);
        }

        [UnityTest]
        public IEnumerator 左にダッシュできるか()
        {
            ResetPosition();
            int waitTime = 50;

            while (waitTime > 0)
            {
                player.Move(Vector3.left, false);
                waitTime--;

                yield return null;
            }

            Transform transform = player.transform;
            Vector3 normal = transform.position;

            ResetPosition();
            waitTime = 50;

            while (waitTime > 0)
            {
                player.Move(Vector3.left, true);
                waitTime--;

                yield return null;
            }

            Vector3 dash = player.transform.position;
            Assert.Greater(normal.x, dash.x);
        }

        [UnityTest]
        public IEnumerator ジャンプできるか()
        {
            ResetPosition();

            yield return new WaitWhile(() => !player.IsGround);

            Vector3 org = player.transform.position;
            player.Jump(true);

            yield return new WaitForSeconds(0.5f);

            Vector3 moved = player.transform.position;
            Assert.Greater(moved.y, org.y);

            yield return null;
        }

        [UnityTest]
        public IEnumerator しゃがむことができるか()
        {
            Vector3 org = player.transform.localScale;
            Vector3 crouched = org;
            int waitTime = 50;

            while (waitTime > 0)
            {
                player.Crouch(true);

                if (waitTime == 25)
                {
                    crouched = player.transform.localScale;
                }

                waitTime--;

                yield return null;
            }

            Assert.Less(crouched.y, org.y);
            player.Crouch(false);
        }

        [UnityTest]
        public IEnumerator 空中ではジャンプできないか()
        {
            yield return new WaitWhile(() => !player.IsGround);

            player.Jump(true);

            yield return new WaitForSeconds(0.1f);

            yield return new WaitWhile(() => player.Velocity.y > 0);

            Vector3 fell = player.transform.position;

            yield return new WaitForSeconds(0.1f);

            player.Jump(true);

            yield return new WaitForSeconds(0.1f);

            Vector3 tryDoubleJump = player.transform.position;

            Assert.Less(tryDoubleJump.y, fell.y);

            yield return null;
        }

        [UnityTest]
        public IEnumerator 時間が進んでいるか()
        {
            DateTime now = scene.Now;

            yield return new WaitForSeconds(1.0f);

            Assert.Greater(scene.Now, now);
        }
    }
}
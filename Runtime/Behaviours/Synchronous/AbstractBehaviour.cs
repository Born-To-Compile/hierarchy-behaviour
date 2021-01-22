﻿using System;
using BornToCompile.HierarchyBehaviour.Build;
using BornToCompile.HierarchyBehaviour.Options;
using Duck.HierarchyBehaviour;
using UnityEngine;

namespace BornToCompile.HierarchyBehaviour.Behaviours.Synchronous
{
	public abstract class AbstractBehaviour<TBehaviour> : IBehaviourBuilder<TBehaviour>
		where TBehaviour : MonoBehaviour
	{
		private readonly BehaviourBuilder builder;

		protected AbstractBehaviour()
		{
			builder = new BehaviourBuilder();
		}

		protected AbstractBehaviour(BehaviourOptionsInitialize options)
		{
			builder = new BehaviourBuilder(options);
		}

		public IBehaviourBuilder<TBehaviour> AsChild(Transform parent, bool worldPositionStays = true)
		{
			builder.AsChild(parent, worldPositionStays);
			return this;
		}

		public IBehaviourBuilder<TBehaviour> Replace<T>(T toReplace) where T : MonoBehaviour
		{
			builder.Replace(toReplace);
			return this;
		}

		public IBehaviourBuilder<TBehaviour> AddComponent<T>(T component) where T : Component
		{
			builder.AddComponent(component);
			return this;
		}

		public IBehaviourBuilder<TBehaviour> DontDestroyOnLoad()
		{
			builder.DontDestroyOnLoad();
			return this;
		}

		public IBehaviourBuilder<TBehaviour> Initialize()
		{
			builder.Initialize();
			return this;
		}

		public TBehaviour Create() => builder.Apply(Instantiate());

		protected abstract TBehaviour Instantiate();

		public TBehaviour Create<TRef>(ref TRef reference) where TRef : MonoBehaviour
		{
			var behaviour = Create();
			reference = behaviour as TRef ?? throw new NullReferenceException();
			return behaviour;
		}
	}

	public abstract class AbstractBehaviour<TBehaviour, TArgs> : IBehaviourBuilder<TBehaviour, TArgs>
		where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
	{
		private readonly BehaviourBuilder<TArgs> builder;

		protected AbstractBehaviour()
		{
			builder = new BehaviourBuilder<TArgs>();
		}

		protected AbstractBehaviour(BehaviourOptionsInitialize<TArgs> options)
		{
			builder = new BehaviourBuilder<TArgs>(options);
		}

		public IBehaviourBuilder<TBehaviour, TArgs> AsChild(Transform parent, bool worldPositionStays = true)
		{
			builder.AsChild(parent, worldPositionStays);
			return this;
		}

		public IBehaviourBuilder<TBehaviour, TArgs> Replace<T>(T toReplace) where T : MonoBehaviour
		{
			builder.Replace(toReplace);
			return this;
		}

		public IBehaviourBuilder<TBehaviour, TArgs> AddComponent<T>(T component) where T : Component
		{
			builder.AddComponent(component);
			return this;
		}

		public IBehaviourBuilder<TBehaviour, TArgs> DontDestroyOnLoad()
		{
			builder.DontDestroyOnLoad();
			return this;
		}

		public IBehaviourBuilder<TBehaviour, TArgs> Initialize(TArgs args)
		{
			builder.Initialize(args);
			return this;
		}

		public TBehaviour Create() => builder.Apply(Instantiate());

		protected abstract TBehaviour Instantiate();

		public TBehaviour Create<TRef>(ref TRef reference) where TRef : MonoBehaviour
		{
			var behaviour = Create();
			reference = behaviour as TRef ?? throw new NullReferenceException();
			return behaviour;
		}
	}
}
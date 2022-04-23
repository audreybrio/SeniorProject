<template>
	<div class="recipe1">
		<router-link to="/Recipe"><button type="back">back</button></router-link>
		<h1>{{ recipe.title }}</h1>
		<p class="desc">{{ recipe.description }}</p>
		<hr/>
		<div class="ingredients">
			<h3>Ingredients</h3>
			<ul>
				<li v-for="(ingredient, i) in recipe.ingredients" :key="i">
					{{ ingredient }}
				</li>
			</ul>
		</div>
		<div class="method">
			<h3>Method Steps</h3>
			<ol>
				<li v-for="(step, i) in recipe.method" :key="i">
					<span v-html="cleanText(step)"></span>
				</li>
			</ol>
		</div>
	</div>
</template>

<script>
export default {
	computed: {
		recipe () {
			return this.$store.state.recipes.find(recipe => recipe.slug === this.$route.params.slug)
		}
	},
	methods: {
		cleanText (text) {
			return text.replace(/\n/g, '<br />')
		}
	}
}
</script>

<style>
.recipe1 {
	padding: 1rem;
	max-width: 768px;
	margin: 0 auto;
  background-color: #06eb24;

}
.desc {
	font-size: 1.125rem;
	line-height: 1.4;
	margin-bottom: 1rem;
}
hr {
	margin-bottom: 1rem;
}
h3 {
	margin-bottom: 1rem;
}
.ingredients {
	padding: 1rem;
	border-radius: 0.5rem;
	margin-bottom: 2rem;
}
.ingredients ul li {
	list-style-position: inside;
	line-height: 1.4;
	margin-bottom: 1rem;
}
.method ol li {
	margin-bottom: 2rem;
	padding-bottom: 1rem;
	list-style-position: inside;
	border-bottom: 1px solid #EEE;
}
</style>
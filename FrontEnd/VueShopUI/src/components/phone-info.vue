<script setup lang="ts">
import { useRouter } from 'vue-router';

const router = useRouter();

const props = defineProps(['phone'])

function handleClick(){
 
    let id = 1;
    // router.push({ path: `/details/${id}` });
    // "if possible use `name` and `params` to benefit from automatic URL encoding"
    router.push({ name: 'details', params: { id } })
}

</script>

<template>
<div id="bordered-info"> 
    <div class="flex-container-row">
        <p id="placeholder-item">X</p>
        <div class="flex-container-column flex-grow-auto">
            <h3>
            {{ phone.type }} - {{ phone.brand.name }}
            </h3>
            <div class="info-container-row">
                <p v-if="phone.price">
                    Price: {{ phone.price }}
                </p>
                <p v-if="phone.stock">
                    Stock: {{ phone.stock }}
                </p>
            </div> 
            <button id="corner-button" @click="handleClick">Show more</button>
        </div>
    </div>
</div>
</template>

<style>
#bordered-info {
    border-style: solid; border-width: thin;
    border-radius: 5px;
    max-width: 20em;
    padding: 0.5em;
}

.info-container-row {
    display: flex; flex-direction: row;
    font-size: small;
}

.info-container-row>*:first-child {
    padding-right: 1em;
}

#corner-button {
    padding: 0.5em;
    margin-top: auto; margin-left: auto;
}

#placeholder-item {
    font-size: xx-large; 
    padding: 1em;
}
</style>
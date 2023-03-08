<script setup lang="ts">

const props = defineProps(['phone'])

function handleClick(){

}

</script>

<template>
<div id="bordered-info" class="flex-justify-left">
    <div class="flex-container-row">
        <p style="font-size: xx-large; padding: 1em;">X</p>
        <div class="flex-container-column">
            <h3>
            {{ phone.type }} - {{ phone.brand.name }}
            </h3>
            <div class="info-container-elements">
                <p v-if="phone.price">
                    Price: {{ phone.price }}
                </p>
                <p v-if="phone.stock">
                    Stock: {{ phone.stock }}
                </p>
            </div> 
            <button id="link-button" @click="handleClick">Show more</button>
            <!-- <router-link to="/details">Show more</router-link> -->
        </div>
    </div>
</div>
</template>

<style>
.info-container-elements {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    font-size: small;
}

.info-container-elements>*:first-child {
    padding-right: 1.5em; 
}

#bordered-info {
    border-style: solid;
    border-radius: 5px;
    border-width: thin;
    max-width: 25em;
}

#link-button {
    padding: 0.5em;    
    margin-left: 10em;
    margin-top: 2.5em;
}
</style>